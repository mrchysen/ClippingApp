using Core.Clustering;
using Core.Clustering.Metrics;
using WindowApp.Infrastructure;

namespace WindowApp.Commands;

public class ClusteringCommand : ICommand
{
    public Task Handle(PlotManager plotManager)
    {
        var clusteringAlgorithm = new KMeansAlgorithm(
            PointDMetricsFactory.CreateMetric(Metric.Euclidean),
            plotManager.Points,
            plotManager.MainWindowContext.ClusterCount);

        var clusters = clusteringAlgorithm.CreateClusters();



        return Task.CompletedTask;
    }
}
