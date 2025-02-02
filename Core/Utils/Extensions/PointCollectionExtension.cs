using Core.Models.Points;
using Core.PointsOrderers;

namespace Core.Utils.Extensions;

public static class PointCollectionExtension
{
    public static IEnumerable<PointD> OrderClockwise(this IEnumerable<PointD> points) 
        => new PointsOrdererByAngle().OrderClockwise(points);
}
