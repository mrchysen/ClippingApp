using System.Drawing;

namespace Core.Models.Points;

public partial struct PointD
{
    public double X { get; set; }
    public double Y { get; set; }
    public PointD(double x, double y)
    {
        X = x;
        Y = y;
    }

    public Point ToDrawingPoint()
        => new Point((int)X, (int)Y);

    public override bool Equals(object? obj) 
        => obj is PointD && this == (PointD)obj;

    public override int GetHashCode() 
        => X.GetHashCode() ^ Y.GetHashCode();

    public override string ToString() 
        => $"{{{X:#.####} {Y:#.####}}}";

    public double Norm() => Math.Sqrt(X * X + Y * Y);

    public PointD Normilized => new PointD(X / Norm(), Y / Norm());
}
