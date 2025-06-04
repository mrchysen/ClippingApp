using Core.Intersection;
using Core.Models.Colors;
using Core.Models.Lines;
using Core.Models.Points;
using Core.Models.Polygons;
using Core.PointInclusionAlgorithms;
using Core.PointsOrderers;
using Core.PolygonServices;

namespace Core.Clippers.RourkeChienPolygonClipper;

/// <summary>
/// https://www.cs.jhu.edu/~misha/Spring16/ORourke82.pdf
/// </summary>
public class RourkeChienPolygonClipper : IClipper
{
    private readonly SegmentIntersector _segmentIntersector;
    private readonly PointPolygonInclusionFinder _pointPolygonInclusionFinder;
    private readonly ConvexPolygonChecker _convexPolygonChecker;

    public RourkeChienPolygonClipper(
        SegmentIntersector? segmentIntersector = null,
        PointPolygonInclusionFinder? pointPolygonInclusionFinder = null,
        ConvexPolygonChecker? convexPolygonChecker = null)
    {
        _segmentIntersector = segmentIntersector ?? new();
        _pointPolygonInclusionFinder = pointPolygonInclusionFinder ?? new();
        _convexPolygonChecker = convexPolygonChecker ?? new();
    }

    public List<Polygon> Clip(List<Polygon> polygons)
    {
        throw new NotImplementedException();
    }

    public List<Polygon> Clip(Polygon polygon1, Polygon polygon2)
    {
        (polygon1, polygon2, bool convexState) =
            EnsureConvexAndUnderClockwise(polygon1, polygon2);

        if (!convexState || polygon1.Points.Count == 0 || polygon2.Points.Count == 0)
            return [polygon1, polygon2];

        List<PointD> newPolygon = new();

        var pN = polygon1.Count;
        var qN = polygon2.Count;

        var pi = 0;
        var qi = 0;

        var p = polygon1.Points[pi];
        var q = polygon2.Points[qi];

        var p_ = polygon1.Points[^1];
        var q_ = polygon2.Points[^1];

        var step = 0;

        string inside = "";

        PointD? firstIntersectionPoint = null;

        do
        {
            var pIn = _segmentIntersector.GetIntersectionPoint(
                new Line(p_, p), new Line(q_, q));

            if (pIn is not null)
            {
                if (firstIntersectionPoint is null)
                {   // remember first intersection
                    firstIntersectionPoint = pIn;
                }
                else
                {
                    if (firstIntersectionPoint == pIn)
                    {
                        break;
                    }
                    else
                    {
                        inside = p.IsIn(new HalfPlane(q_, q)) ? "P" : "Q";
                    }
                }

                newPolygon.Add(pIn.Value);
            }

            if (((q - q_) * (p - p_)) >= 0)
            {
                if (p.IsIn(new HalfPlane(q_, q)))
                {
                   if (inside == "Q")
                        newPolygon.Add(q);
                    q_ = q;
                    qi = (qi + 1) % qN;
                    q = polygon2.Points[qi];
                }
                else
                {
                    if (inside == "P")
                        newPolygon.Add(p);
                    p_ = p;
                    pi = (pi + 1) % pN;
                    p = polygon1.Points[pi];
                }
            }
            else
            {
                if (q.IsIn(new HalfPlane(p_, p))) 
                {
                    if (inside == "P")
                        newPolygon.Add(p);
                    p_ = p;
                    pi = (pi + 1) % pN;
                    p = polygon1.Points[pi];
                }
                else
                {
                    if (inside == "Q")
                        newPolygon.Add(q);
                    q_ = q;
                    qi = (qi + 1) % qN;
                    q = polygon2.Points[qi];
                }
            }

            step++;
        }
        while (2 * (pN + qN) > step);

        if (newPolygon.Count == 0)
        {
            if (_pointPolygonInclusionFinder.CheckPointInsidePolygon(p, polygon2))
                return [polygon1];
            else if (_pointPolygonInclusionFinder.CheckPointInsidePolygon(q, polygon1))
                return [polygon2];
            else
                return [];
        }

        return [new Polygon(newPolygon, CoreColor.IntersectColors(polygon1.Color, polygon2.Color))];
    }

    private (Polygon polygon1, Polygon polygon2, bool convexState)
        EnsureConvexAndUnderClockwise(
        Polygon polygon1,
        Polygon polygon2)
    {
        var convexState = _convexPolygonChecker.IsConvex(polygon1) &&
                          _convexPolygonChecker.IsConvex(polygon2);

        if (!convexState)
            return (polygon1, polygon2, false);

        polygon1.Points = polygon1.Points.OrderClockwise().Reverse().ToList(); 
        polygon2.Points = polygon2.Points.OrderClockwise().Reverse().ToList();

        return (polygon1, polygon2, true);
    }
}

file record HalfPlane(PointD p_, PointD p);

file static class PointDExtension
{
    public static bool IsIn(this PointD point, HalfPlane hp)
    {
        return ((hp.p - hp.p_) * (point - hp.p_)) >= 0;
    }
}
