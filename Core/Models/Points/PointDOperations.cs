namespace Core.Models.Points;

public partial struct PointD
{
    public static bool operator ==(PointD a, PointD b)
    {
        return a.X == b.X && a.Y == b.Y;
    }
    public static bool operator !=(PointD a, PointD b)
    {
        return !(a == b);
    }

    // Point = Vector operations
    public static PointD operator +(PointD a, PointD b) 
        => new PointD(a.X + b.X, a.Y + b.Y);

    public static PointD operator *(PointD a, double value)
        => new PointD(a.X * value, a.Y * value);

    public static PointD operator *(double value, PointD a)
        => new PointD(a.X * value, a.Y * value);

    public static PointD operator -(PointD a, PointD b)
        => a + (-1)*b;

    public static double operator *(PointD a, PointD b) // vec multi
        => a.X * b.Y - a.Y * b.X;
}
