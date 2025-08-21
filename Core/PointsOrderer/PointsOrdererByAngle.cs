using Core.Models.Points;

namespace Core.PointsOrderers;

public static class PointsOrdererByAngle
{
    /// <summary>
    /// Sort points clockwise
    /// </summary>
    /// <param name="points"></param>
    /// <returns></returns>
	public static IEnumerable<PointD> OrderClockwise(this IEnumerable<PointD> points)
	{
        var _points = new List<PointD>(points);

        if (!_points.Any())
			return Enumerable.Empty<PointD>();

        var center = GetCenterMass(_points);

        return _points.OrderBy(v => ReverseAtan(v.Y - center.Y, v.X - center.X));
	}

    /// <summary>
    /// Returns angles of clockwise sorted points
    /// See GetAnglesExplorations.png in this folder
    /// </summary>
    /// <param name="points"></param>
    /// <returns></returns>
    public static IEnumerable<double> GetAngles(this IEnumerable<PointD> points)
    {
        var _points = new List<PointD>(points);

        if (!_points.Any())
            return Enumerable.Empty<double>();

        var center = GetCenterMass(_points);

        return _points.Select(v => ReverseAtan(v.Y - center.Y, v.X - center.X));
    }

    private static PointD GetCenterMass(IEnumerable<PointD> points) 
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

    /// <summary>
    /// Give values from [0, 2Pi].
    /// ReverseAtan() = 0 if Math.Atan2 = Pi
    /// ReverseAtan() = Pi if Math.Atan2 = 0
    /// ReverseAtan() = 2Pi if Math.Atan2 = -Pi
    /// </summary>
    /// <param name="y"></param>
    /// <param name="x"></param>
    /// <returns></returns>
    public static double ReverseAtan(double y, double x)
    {
        double angel = Math.Atan2(y, x);

        if (y > 0)
            return Math.PI - angel;
        else if (y == 0 && x > 0)
            return Math.PI;
        else if (y == 0 && x < 0)
            return 0;
        else // y < 0
            return Math.PI + Math.Abs(angel);
    }
}
