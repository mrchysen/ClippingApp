namespace Core.Models.Points;

public static class PointDHelper
{
    /// <summary>
    /// Returns the highest point on the right"
    /// </summary>
    /// <param name="p1"></param>
    /// <param name="p2"></param>
    /// <returns></returns>
    public static PointD MaxPointD(PointD p1, PointD p2)
    {
        if (p1.Y > p2.Y)
        {
            return p1;
        }
        else if (p1.Y == p2.Y)
        {
            if (p1.X > p2.X)
            {
                return p1;
            }
            else
            {
                return p2;
            }
        }
        else
        {
            return p2;
        }
    }

    /// <summary>
    /// Returns the lowest point on the left.
    /// </summary>
    /// <param name="p1"></param>
    /// <param name="p2"></param>
    /// <returns></returns>
    public static PointD MinPointD(PointD p1, PointD p2)
    {
        if (p1.Y > p2.Y)
        {
            return p2;
        }
        else if (p1.Y == p2.Y)
        {
            if (p1.X > p2.X)
            {
                return p2;
            }
            else
            {
                return p1;
            }
        }
        else
        {
            return p1;
        }
    }
}
