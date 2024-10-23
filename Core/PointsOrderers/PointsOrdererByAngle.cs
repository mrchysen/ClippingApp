using Core.Models;

namespace Core.PointsOrderers;

public class PointsOrdererByAngle
{
	public IEnumerable<PointD> OrderClockwise(IEnumerable<PointD> points)
	{
		if (!points.Any())
			return Enumerable.Empty<PointD>();

        var center = GetCenterMass(points);

        return points.OrderBy(v => Math.Atan2(v.Y - center.Y, v.X - center.X));
	}

    public IEnumerable<double> GetAngels(IEnumerable<PointD> points)
    {
        if (!points.Any())
            return Enumerable.Empty<double>();

        var center = GetCenterMass(points);

        return points.Select(v => Math.Atan2(v.Y - center.Y, v.X - center.X));
    }

    public PointD GetCenterMass(IEnumerable<PointD> points) 
    {
        double mX = 0;
        double mY = 0;
        int n = 0;

        foreach (var p in points)
        {
            mX += p.X;
            mY += p.Y;
            n++;
        }

        mX /= n;
        mY /= n;

        return new(mX, mY);
    }

    public double ReverseAtan(double y, double x)
    {
        double angel = Math.Atan2(y, x);

        
    }
}
