using Core.Models;

namespace Core.PointsOrderers;

public class PointsOrdererByAngle
{
	public List<PointD> OrderClockwise(List<PointD> points)
	{
		double mX = 0;
		double my = 0;

		foreach (var p in points)
		{
			mX += p.X;
			my += p.Y;
		}

		mX /= points.Count;
		my /= points.Count;

		return points.OrderBy(v => Math.Atan2(v.Y - my, v.X - mX)).ToList();
	}
}
