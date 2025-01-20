using Core.Intersection;
using Core.Models;
using Core.Models.Points;
using Core.Models.Polygons;

namespace Core.Clippers.RourkeChienPolygonClipper;

public class RourkeChienPolygonClipper : IClipper
{
    private readonly SegmentIntersector _segmentIntersector;

    public RourkeChienPolygonClipper(SegmentIntersector segmentIntersector)
    {
        _segmentIntersector = segmentIntersector;
    }

    public List<Polygon> Clip(List<Polygon> polygons)
    {
        throw new NotImplementedException();
    }

    public List<Polygon> Clip(Polygon polygon1, Polygon polygon2)
    {
        if(polygon1.Points.Count == 0 || polygon2.Points.Count == 0)
            return [polygon1, polygon2];

        List<PointD> newPolygon = new();

        var pN = polygon1.Count;
        var qN = polygon2.Count;

        var pi = 0;
        var qi = 0;

        var p = polygon1.Points[pi];
        var q = polygon2.Points[qi];

        var p_ = polygon1.Points[PrevIndex(pi, pN)];
        var q_ = polygon2.Points[PrevIndex(qi, qN)];

        var step = 0;

        string inside = "";

        PointD? firstIntersectionPoint = null;

        do
        {
            var pIn = _segmentIntersector.GetIntersectionPoint(
                new Line(p_, p), new Line(q_, q));

            if (pIn is not null)
            {
                if(firstIntersectionPoint is null)
                {
                    firstIntersectionPoint = pIn;
                }
                else
                {
                    if(firstIntersectionPoint == pIn)
                    {
                        break;
                    }
                    else
                    {
                        newPolygon.Add(pIn.Value);
                        if (((q - q_) * (p - q_)) >= 0)
                        {
                            inside = "P";
                        }
                        else
                        {
                            inside = "Q";
                        }
                    }
                }
                
            }

            if (((q - q_)*(p - p_)) >= 0)
            {
                if (((q - q_)*(p - q_)) >= 0)
                {
                    // q
                    if (inside == "Q")
                        newPolygon.Add(q);
                    q_ = q;
                    qi = NextIndex(qi, qN);
                    q = polygon2.Points[qi];
                }
                else
                {
                    // p
                    if (inside == "P")
                        newPolygon.Add(p);
                    p_ = p;
                    pi = NextIndex(pi, pN);
                    p = polygon1.Points[pi];
                }
            }
            else
            {
                if (((p - p_)*(q - p_)) >= 0)
                {
                    // p
                    if (inside == "P")
                        newPolygon.Add(p);
                    p_ = p;
                    pi = NextIndex(pi, pN);
                    p = polygon1.Points[pi];
                }
                else
                {
                    // q
                    if (inside == "Q")
                        newPolygon.Add(q);
                    q_ = q;
                    qi = NextIndex(qi, qN);
                    q = polygon2.Points[qi];
                }
            }

            step++;
        }
        while (3 * (pN + qN) > step);

        return [new Polygon(newPolygon)];
    }

    private int PrevIndex(int i, int N)
        => i == 0  ? N - 1 : i - 1;

    private int NextIndex(int i, int N)
    => i == N - 1 ? 0 : i + 1;
}
