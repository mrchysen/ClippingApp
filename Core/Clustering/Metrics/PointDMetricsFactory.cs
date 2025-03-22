using Core.Models.Points;

namespace Core.Clustering.Metrics;

public static class PointDMetricsFactory
{
    public static IMetric<double, PointD> CreateMetric(Metric metricName)
        => metricName switch
        {
            Metric.Euclidean => new EuclideanMetric(),
            _ => throw new Exception("Not supported metric") 
        };
}

public enum Metric{
    Euclidean
}
