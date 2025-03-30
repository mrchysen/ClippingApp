using Core.Models.Points;

namespace Core.Clustering.Metrics;

public class ChebyshevMetric : IMetric<double, PointD>
{
    public double Compute(PointD p1, PointD p2)
        => Math.Max(Math.Abs(p1.X - p2.X), Math.Abs(p1.Y - p2.Y));
}
