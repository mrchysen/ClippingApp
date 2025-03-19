namespace Core.Clustering.Metrics;

public interface IMetric<TResult, TParam>
{
    TResult Compute(TParam p1, TParam p2);
}
