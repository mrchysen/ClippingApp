using Core.Models;
using Core.Models.Polygons;
using Core.Utils.Equalizers;

namespace Core.Intersection;

public class LineAndPolygonIntersector
{
	public double Epsilon { get; set; } = 0.001d;

	public EqualizerBase<double> Equalizer { get; set; } = new DoubleEqualizer(0.001);

	public LineIntersector LineIntersector { get; set; } = new();

    public List<PointD> GetIntersectionPoint(Line line, Polygon polygon)
	{
		List<PointD> intersectionPoints = new List<PointD>();

		for (int i = 0; i < polygon.Points.Count; i++)
		{
			int next = (i + 1 == polygon.Points.Count) ? 0 : i + 1;

			var point = LineIntersector.GetIntersectionPoint(line, new Line(polygon.Points[i], polygon.Points[next]));
			
			if (point != null) intersectionPoints.Add(point.Value);
		}

		return intersectionPoints;
	}
}
