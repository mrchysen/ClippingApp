using System.Drawing;

namespace Core.Models;

public struct PointD
{
	public double X { get; set; }
	public double Y { get; set; }
	public PointD(double x, double y)
	{
		X = x;
		Y = y;
	}
	public Point ToPoint()
	{
		return new Point((int)X, (int)Y);
	}
	public override bool Equals(object? obj)
	{
		return obj is PointD && this == (PointD)obj;
	}
	public override int GetHashCode()
	{
		return X.GetHashCode() ^ Y.GetHashCode();
	}
	public static bool operator ==(PointD a, PointD b)
	{
		return a.X == b.X && a.Y == b.Y;
	}
	public static bool operator !=(PointD a, PointD b)
	{
		return !(a == b);
	}

	public override string ToString() => $"{{{X:#.####} {Y:#.####}}}";
}
