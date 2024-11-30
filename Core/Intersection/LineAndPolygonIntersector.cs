using Core.Models;
using Core.Models.Polygons;
using Core.Utils.Equalizers;

namespace Core.Intersection;

public class LineAndPolygonIntersector
{
	private readonly LineIntersector _lineIntersector;

    public LineAndPolygonIntersector(LineIntersector lineIntersector)
		=> _lineIntersector = lineIntersector;

    public List<PointD> GetIntersectionPoint(Line line, Polygon polygon)
	{
		List<PointD> intersectionPoints = new List<PointD>();

		for (int i = 0; i < polygon.Points.Count; i++)
		{
			int next = (i + 1 == polygon.Points.Count) ? 0 : i + 1;

			var point = _lineIntersector.GetIntersectionPoint(line, new Line(polygon.Points[i], polygon.Points[next]));
			
			if (point != null) intersectionPoints.Add(point.Value);
		}

		return intersectionPoints;
	}
}
