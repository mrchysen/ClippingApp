using Core.Models.DoubleLinkedLists;
using Core.Models.Lines;
using Core.Models.Points;
using Core.Models.Polygons;

namespace Core.Intersection;

// Show how many points in intersection of segment and polygon
public class SegmentAndPolygonIntersector
{
    private readonly SegmentIntersector _segmentIntersector;

    public SegmentAndPolygonIntersector(SegmentIntersector? lineIntersector = null)
    {
        _segmentIntersector = lineIntersector ?? new();
    }

    public List<PointD> GetIntersectionPoint(Line line, DoubleLinkedList<PointD> polygon)
    {
        List<PointD> intersectionPoints = [];

        var firstPoint = polygon.Head;
        var currentPoint = polygon.Head;

        do
        {
            var point = _segmentIntersector.GetIntersectionPoint(line,
                new Line(currentPoint.Value, currentPoint.Next.Value));

            if (point != null) intersectionPoints.Add(point.Value);

            currentPoint = currentPoint.Next;
        }
        while (currentPoint != firstPoint);

        return intersectionPoints;
    }

    public List<PointD> GetIntersectionPoint(Line line, Polygon polygon)
    {
        List<PointD> intersectionPoints = [];

        for (int i = 0; i < polygon.Points.Count; i++)
        {
            int next = i + 1 == polygon.Points.Count ? 0 : i + 1;

            var point = _segmentIntersector.GetIntersectionPoint(line, new Line(polygon.Points[i], polygon.Points[next]));

            if (point != null) intersectionPoints.Add(point.Value);
        }

        return intersectionPoints;
    }
}
