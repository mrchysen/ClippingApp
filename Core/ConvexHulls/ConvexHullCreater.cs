using Core.Models;
using Core.Utils.Equalizers;

namespace Core.ConvexHulls;

public class ConvexHullCreator
{
    private DoubleEqualizer _equalizer = new(0.0001d);

    /// <summary>
    /// 0 - pq и qr коллинеарны.
    /// 1 - qr достигается pq поворотом pq по часовой.
    /// 2 - qr достигается pq поворотом pq против часовой.
    /// </summary>
    /// <param name="p"></param>
    /// <param name="q"></param>
    /// <param name="r"></param>
    /// <returns></returns>
    public int Orientation(PointD p, PointD q, PointD r)
    {
        double val = (q.Y - p.Y) * (r.X - q.X) -
                     (q.X - p.X) * (r.Y - q.Y);

        if (_equalizer.IsEquals(val, 0)) return 0;

        return (val > 0) ? 1 : 2;
    }

    /// <summary>
    /// Convex Hull using Graham Scan
    /// </summary>
    /// <param name="points"></param>
    /// <returns></returns>
    public List<PointD> ConvexHull(List<PointD> points)
    {
        int n = points.Count;

        if (n < 3) return new List<PointD>();

        List<PointD> hull = new List<PointD>();

        int l = 0;
        for (int i = 1; i < n; i++)
            if (points[i].X < points[l].X)
                l = i;

        int p = l, q;
        do
        {
            hull.Add(points[p]);

            q = (p + 1) % n;

            for (int i = 0; i < n; i++)
            {
                if (Orientation(points[p], points[i], points[q]) == 2)
                    q = i;
            }

            p = q;

        } while (p != l);

        return hull;
    }
}