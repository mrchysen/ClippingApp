using Core.Clustering;
using Core.Clustering.Metrics;
using Core.Colors;
using ScottPlot;
using WindowApp.Infrastructure;
using Application.PlotExtensions;

namespace WindowApp.Commands;

public class ClusteringCommand : ICommand
{
    public async Task Handle(PlotManager plotManager)
    {
        var task = Task.Run(() =>
        {
            var clusteringAlgorithm = new KMeansAlgorithm(
            PointDMetricsFactory.CreateMetric(Metric.Euclidean),
            plotManager.Points,
            plotManager.MainWindowContext.ClusterCount);

            var clusters = clusteringAlgorithm.CreateClusters();

            var colors = Enumerable.Range(0, clusters.Count)
                .Select(el => RandomColor.Get())
                .ToList();

            return (clusters, colors);
        });
        
        var (clusters, colors) = await task;

        plotManager.Plot.Clear();
        plotManager.Clusters.Clear();
        plotManager.Clusters.AddRange(clusters);

        for (int i = 0; i < clusters.Count; i++)
        {
            plotManager.Plot.AddMarkers(clusters[i].Points, 
                colors[i],
                14);

            plotManager.Plot.AddOneMarker(clusters[i].Centroid,
                colors[i],
                MarkerShape.Eks,
                16);
        }

        plotManager.Plot.Axes.AutoScale();
        plotManager.WpfPlot.Refresh();
    }
}
