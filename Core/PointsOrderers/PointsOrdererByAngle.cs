using Core.Models;

namespace Core.PointsOrderers;

public class PointsOrdererByAngle
{
	public IEnumerable<PointD> OrderClockwise(IEnumerable<PointD> points)
	{
		if (!points.Any())
			return Enumerable.Empty<PointD>();

		double mX = 0;
		double my = 0;
		int n = 0;

		foreach (var p in points)
		{
			mX += p.X;
			my += p.Y;
			n++;
		}

		mX /= n;
		my /= n;

		return points.OrderBy(v => Math.Atan2(v.Y - my, v.X - mX));
	}
}
