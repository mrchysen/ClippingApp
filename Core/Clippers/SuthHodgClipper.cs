using Core.Colors;
using Core.Intersection;
using Core.Models;
using Core.Models.Polygons;
using Core.Utils.Equalizers;

namespace Core.Clippers;

public class SuthHodgClipper : IClipper
{
    private readonly LineIntersector _lineIntersector;

    public SuthHodgClipper(LineIntersector lineIntersector)
    {
        _lineIntersector = lineIntersector;
    }

    private void Clip(List<PointD> polyPoints, PointD c1, PointD c2)
    {
        List<PointD> newPoints = new List<PointD>();

        for (int i = 0; i < polyPoints.Count; i++)
        {
            int k = (i + 1) % polyPoints.Count;
            PointD ip = polyPoints[i];
            PointD kp = polyPoints[k];

            // это векторное произведение отсекающего вектора и вектора конца от конца 
            double iPos = (c2.X - c1.X) * (ip.Y - c1.Y) - (c2.Y - c1.Y) * (ip.X - c1.X);
            // это векторное произведение отсекающего вектора и вектора
            double kPos = (c2.X - c1.X) * (kp.Y - c1.Y) - (c2.Y - c1.Y) * (kp.X - c1.X); 

            if (iPos < 0 && kPos < 0)
            {
                newPoints.Add(kp);
            }
            else if (iPos >= 0 && kPos < 0)
            {
                var p = _lineIntersector.GetIntersectionPoint(new(c1, c2), new(ip, kp));

                if (p == null)
                    continue;

                newPoints.Add(p!.Value);
                newPoints.Add(kp);
            }
            else if (iPos < 0 && kPos >= 0)
            {
                var p = _lineIntersector.GetIntersectionPoint(new(c1, c2), new(ip, kp));

                if (p == null)
                    continue;

                newPoints.Add(ip);
                newPoints.Add(p!.Value);
            }
        }

        polyPoints.Clear();
        polyPoints.AddRange(newPoints);
    }

    public List<PointD> SuthHodgClip(List<PointD> polyPoints, List<PointD> clipperPoints)
    {
        List<PointD> poly = new List<PointD>(polyPoints);

        for (int i = 0; i < clipperPoints.Count; i++)
        {
            int k = (i + 1) % clipperPoints.Count;
            Clip(poly, clipperPoints[i], clipperPoints[k]);
        }

        return poly;
    }

    public List<Polygon> Clip(List<Polygon> polygons)
    {
        // Алгоритм работает, когда у нас есть выпуклый полигон.
        // Но в этом случае непонятно, как найти выпуклый полигон.

        return [new Polygon() {
            Color = RandomColor.Get(),
            Points = [
                new (1, 1),
                new (1, 10),
                new (10, 10),
                new (10, 1),
                ]
        }];
    }

    /// <summary>
    /// Пересечение любого и выпуклого полигона.
    /// </summary>
    /// <param name="polygon1"></param>
    /// <param name="polygon2"></param>
    /// <returns></returns>
    public List<Polygon> Clip(Polygon polygon1, Polygon polygon2)
    {
        //return [new Polygon(SuthHodgClip(polygon1.Points, polygon2.Points)) { Color = RandomColor.Get() }];
    
        return [new Polygon() {
            Color = RandomColor.Get(),
            Points = [
                new (1, 1),
                new (1, 10),
                new (10, 10),
                new (10, 1),
                ]
        }];
    }
}
