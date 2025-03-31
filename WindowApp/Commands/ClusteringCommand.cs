using Core.Clustering;
using Core.Clustering.Metrics;
using Core.Colors;
using ScottPlot;
using WindowApp.Infrastructure;
using Application.PlotExtensions;
using WindowApp.Components.Plates.Clustering;
using Core.Models.Points;

namespace WindowApp.Commands;

public class ClusteringCommand : IMainWindowCommand
{
    private PlotManager _plotManager;
    private ClusteringViewModel _viewModel;
    private IMetric<double, PointD> _metric;

    public ClusteringCommand(
        PlotManager plotManager, 
        ClusteringViewModel viewModel,
        IMetric<double, PointD> metric)
    {
        _plotManager = plotManager;
        _viewModel = viewModel;
        _metric = metric;
    }

    public async Task Handle()
    {
        if (_plotManager.Points.Count == 0)
            return;

        var task = Task.Run(() =>
        {
            var clusteringAlgorithm = new KMeansAlgorithm(
            _metric,
            _plotManager.Points,
            _viewModel.ClusterCount,
            _metric is CosMetric ? true : false); // условия на ограничение по кол-ву итераций

            var clusters = clusteringAlgorithm.CreateClusters();

            

            return clusters;
        });
        
        var clusters = await task;

        _plotManager.Plot.Clear();
        _plotManager.Clusters.Clear();
        _plotManager.Clusters.AddRange(clusters);

        _plotManager.DrawClusters();
    }
}
