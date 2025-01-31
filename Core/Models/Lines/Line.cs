using Core.Models.Points;

namespace Core.Models.Lines;

public class Line
{
    public PointD Point1 { get; set; }

    public PointD Point2 { get; set; }

    public double A => Point2.Y - Point1.Y;

    public double B => Point1.X - Point2.X;

    public double C => Point1.Y * Point2.X - Point1.X * Point2.Y;

    public Line(PointD point1, PointD point2)
    {
        Point1 = point1;
        Point2 = point2;
    }
}
