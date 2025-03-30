using Core.Models.Points;

namespace Core.Clustering.Metrics;

public class CosMetric : IMetric<double, PointD>
{
    public double Compute(PointD p1, PointD p2)
        => (p1.X * p2.X + p1.Y * p2.Y) / (p1.Norm() * p2.Norm() + 0.000001);
}
