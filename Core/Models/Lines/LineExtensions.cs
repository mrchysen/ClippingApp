using Core.Models.Points;

namespace Core.Models.Lines;

public static class LineExtensions
{
    public static double MinDistance(this Line line1, PointD point) 
        => Math.Abs(line1.DirectingVector * (line1.Point1 - point)) / line1.DirectingVector.Norm();
}
