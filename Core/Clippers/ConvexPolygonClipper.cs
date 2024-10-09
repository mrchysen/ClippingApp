using Core.Intersection;
using Core.Models;
using Core.Models.Polygons;
using Core.PointInclusionAlgorithms;
using Core.PointsOrderers;
using Core.Utils.Equalizers;

namespace Core.Clippers;

public class ConvexPolygonClipper : IClipper
{
	public PointDEqualizer pointEqualizer { get; set; } = new(0.001d);

	public PointInclusion pointInclusion { get; set; } = new();

	public LineAndPolygonIntersector lineAndPolygonIntersector { get; set; } = new();

	public PointsOrdererByAngle pointsOrdererByAngle { get; set; } = new();

    public List<Polygon> Clip(List<Polygon> polygons)
	{
		List<Polygon> result = new List<Polygon>();

		if(polygons.Count < 2)
			return result;

		var polygon = polygons[0];

		for (int i = 1; i < polygons.Count; i++) 
		{
			polygon = Clip(polygons[i], polygon)[0];
		}

		return [polygon];
	}

	public List<Polygon> Clip(Polygon polygon1, Polygon polygon2)
	{
		List<PointD> clippedCorners = new List<PointD>();
		    
		for (int i = 0; i < polygon1.Points.Count; i++)
		{
			if (pointInclusion.CheckPointInsidePolygon(polygon1.Points[i], polygon2))
				AddPoints(clippedCorners, new List<PointD> { polygon1.Points[i] });
		}
		
		for (int i = 0; i < polygon2.Points.Count; i++)
		{
			if (pointInclusion.CheckPointInsidePolygon(polygon2.Points[i], polygon1))
				AddPoints(clippedCorners, new List<PointD> { polygon2.Points[i] });
		}
		
		for (int i = 0, next = 1; i < polygon1.Points.Count; i++, next = (i + 1 == polygon1.Points.Count) ? 0 : i + 1)
		{
			AddPoints(clippedCorners, lineAndPolygonIntersector.GetIntersectionPoint(new Line(polygon1.Points[i], polygon1.Points[next]), polygon2));
		}

		return new List<Polygon>() { new Polygon(pointsOrdererByAngle.OrderClockwise(clippedCorners).ToList()) };
	}

	private void AddPoints(List<PointD> pool, List<PointD> newpoints)
	{
		foreach (PointD newpoint in newpoints)
		{
			bool oldPointFlag = false;

			foreach (PointD point in pool)
			{
				if (pointEqualizer.IsEquals(newpoint, point))
				{
					oldPointFlag = true;
					break;
				}
			}

			if (!oldPointFlag) pool.Add(newpoint);
		}
	}
}
