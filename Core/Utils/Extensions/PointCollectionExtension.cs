using Core.Models;
using Core.PointsOrderers;

namespace Core.Utils.Extensions;

public static class PointCollectionExtension
{
	public static IEnumerable<PointD> OrderClockwise(this IEnumerable<PointD> points)
	{
		PointsOrdererByAngle orderer = new();

		return orderer.OrderClockwise(points);
	}
}
