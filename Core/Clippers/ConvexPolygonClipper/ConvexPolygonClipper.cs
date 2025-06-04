using Core.Intersection;
using Core.Models.Colors;
using Core.Models.Lines;
using Core.Models.Points;
using Core.Models.Polygons;
using Core.PointInclusionAlgorithms;
using Core.PointsOrderers;
using Core.Utils.Equalizers;

namespace Core.Clippers.ConvexPolygonClipper;

public class ConvexPolygonClipper : IClipper
{
    private readonly PointDEqualizer _pointEqualizer;
    private readonly PointPolygonInclusionFinder _pointInclusion;
    private readonly SegmentAndPolygonIntersector _lineAndPolygonIntersector;

    public ConvexPolygonClipper(
        PointDEqualizer? pointEqualizer = null, 
        PointPolygonInclusionFinder? pointInclusion = null, 
        SegmentAndPolygonIntersector? lineAndPolygonIntersector = null)
    {
        _pointEqualizer = pointEqualizer ?? new();
        _pointInclusion = pointInclusion ?? new();
        _lineAndPolygonIntersector = lineAndPolygonIntersector ?? new();
    }

    public List<Polygon> Clip(List<Polygon> polygons)
    {
        List<Polygon> result = new List<Polygon>();

        if (polygons.Count < 2)
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
        List<PointD> clippedPoints = [];

        for (int i = 0; i < polygon1.Points.Count; i++)
        {
            if (_pointInclusion.CheckPointInsidePolygon(polygon1.Points[i], polygon2))
                AddPointsWithoutDuplicate(clippedPoints, [polygon1.Points[i]]);
        }

        for (int i = 0; i < polygon2.Points.Count; i++)
        {
            if (_pointInclusion.CheckPointInsidePolygon(polygon2.Points[i], polygon1))
                AddPointsWithoutDuplicate(clippedPoints, [polygon2.Points[i]]);
        }

        for (int i = 0, next = 1; i < polygon1.Points.Count; i++, next = i + 1 == polygon1.Points.Count ? 0 : i + 1)
        {
            AddPointsWithoutDuplicate(clippedPoints,
                _lineAndPolygonIntersector.GetIntersectionPoint(
                    new Line(polygon1.Points[i], polygon1.Points[next]), 
                    polygon2));
        }

        return [
            new Polygon(clippedPoints.OrderClockwise().ToList(), 
                CoreColor.IntersectColors(polygon1.Color, polygon2.Color)) ];
    }

    private void AddPointsWithoutDuplicate(List<PointD> clippedPoints, List<PointD> newPoints)
    {
        foreach (PointD newPoint in newPoints)
        {
            bool oldPointFlag = false;

            foreach (PointD point in clippedPoints)
            {
                if (_pointEqualizer.IsEquals(newPoint, point))
                {
                    oldPointFlag = true;
                    break;
                }
            }

            if (!oldPointFlag) clippedPoints.Add(newPoint);
        }
    }
}
