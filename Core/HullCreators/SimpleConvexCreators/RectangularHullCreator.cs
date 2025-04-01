using Core.Models.Points;
using Core.Models.Polygons;
using System.Data;
using System.Diagnostics;

namespace Core.HullCreators.SimpleConvexCreators;

public class RectangularHullCreator : IConvexHullCreator
{
    private readonly PointD _centroid;

    public RectangularHullCreator(PointD centroid)
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

        var normal = new PointD((-1)*(maxDistancePoint - _centroid).Y, 
            (maxDistancePoint - _centroid).X).Normilized * maxDistance;

        List<PointD> polygonPoints = [
            _centroid + (maxDistancePoint - _centroid) * (-1) + normal,
            _centroid + (maxDistancePoint - _centroid) + normal,
            _centroid + (maxDistancePoint - _centroid) + normal * (-1),
            _centroid + (maxDistancePoint - _centroid) * (-1) + normal * (-1)
            ];

        Debug.WriteLine(maxDistancePoint + " " + AB);

        return new Polygon(polygonPoints);
    }
}
