using Core.Models.Polygons;
using Core.Utils.Equalizers;
using System.Diagnostics.Contracts;

namespace Core.PolygonServices;

public interface IConvexPolygonChecker
{
    bool IsConvex(Polygon polygon);
}

public class ConvexPolygonChecker : IConvexPolygonChecker
{
    public DoubleEqualizer _equalizer;

    public ConvexPolygonChecker(DoubleEqualizer? equalizer = null)
    {
        _equalizer = equalizer ?? new();
    }

    [Pure]
    public bool IsConvex(Polygon polygon)
    {
        int N = polygon.Count;

        if(N < 3)
            return false;

        double clockwiseState = 0;

        for (int i = 0; i < N; i++) 
        { 
            var p1 = polygon.Points[i];
            var p2 = polygon.Points[(i + 1) % N];
            var p3 = polygon.Points[(i + 2) % N];

            var vectorProduction = (p2 - p1) * (p3 - p2);

            if (vectorProduction * clockwiseState < 0)
            {
                return false;
            }
            else
            {
                clockwiseState = vectorProduction;
            }
        }

        return true;
    }
}
