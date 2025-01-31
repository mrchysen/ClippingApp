using Core.Intersection;
using Core.Models.Lines;
using Core.Models.Points;
using Core.Models.Polygons;

namespace Core.Clippers.ConvexPolygonClipper;

public class SegmentAndPolygonIntersector
{
    private readonly SegmentIntersector _segmentIntersector;

    public SegmentAndPolygonIntersector(SegmentIntersector lineIntersector)
        => _segmentIntersector = lineIntersector;

    public List<PointD> GetIntersectionPoint(Line line, Polygon polygon)
    {
        List<PointD> intersectionPoints = new List<PointD>();

        for (int i = 0; i < polygon.Points.Count; i++)
        {
            int next = i + 1 == polygon.Points.Count ? 0 : i + 1;

            var point = _segmentIntersector.GetIntersectionPoint(line, new Line(polygon.Points[i], polygon.Points[next]));

            if (point != null) intersectionPoints.Add(point.Value);
        }

        return intersectionPoints;
    }
}
