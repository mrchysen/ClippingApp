using Core.Models.Points;
using Core.Models.Polygons;

namespace Core.HullCreators.SimpleConvexCreators;

public class EllipseHullCreator : IConvexHullCreator
{
    private readonly PointD _centroid;

    public const int Delta = 12;

    public EllipseHullCreator(PointD centroid)
    {
        _centroid = centroid;
    }

    public Polygon CreateHull(List<PointD> points)
    {
        var maxDistancePoint = points.Aggregate((p1, p2)
            => (_centroid - p1).Norm() > (_centroid - p2).Norm() ? p1 : p2);

        double AB = (maxDistancePoint - _centroid).Norm();
        double maxDistance = Math.Abs((points[0] - _centroid) * (points[0] - maxDistancePoint))
            / (AB);

        for (int i = 0; i < points.Count; i++)
        {
            var currentPoint = points[i];
            var currentDistance = Math.Abs((currentPoint - _centroid) * (currentPoint - maxDistancePoint)) / (AB);

            if (currentDistance > maxDistance)
            {
                maxDistance = currentDistance;
            }
        }

        var normal = new PointD((-1) * (maxDistancePoint - _centroid).Y,
            (maxDistancePoint - _centroid).X).Normilized * maxDistance;

        double Rx = AB;
        double Ry = maxDistance;
        double cos = (maxDistancePoint - _centroid).X / AB;
        double sin = (maxDistancePoint - _centroid).Y / AB;
        
        List<PointD> polygonPoints = new();

        for (int i = 0; i < Delta; i++)
        {
            double alpha = i * (2 * Math.PI / Delta);

            polygonPoints.Add(new PointD()
            {
                X = _centroid.X + Rx * Math.Cos(alpha) * cos - Ry * Math.Sin(alpha) * sin,
                Y = _centroid.Y + Rx * Math.Cos(alpha) * sin + Ry * Math.Sin(alpha) * cos
            });
        }

        return new Polygon(polygonPoints);
    }
}
