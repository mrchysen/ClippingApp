using Core.Clustering;
using Core.HullCreators.QuickHull;
using WindowApp.Infrastructure;

namespace WindowApp.Commands;

public class CreateConvexHullsOnClustersCommand : IMainWindowCommand
{
    private PlotManager _plotManager;

    public CreateConvexHullsOnClustersCommand(PlotManager plotManager)
    {
        _plotManager = plotManager;
    }

    public async Task Handle()
    {
        _plotManager.ClearOnlyPlot();
        _plotManager.Polygons.Clear();
        var hullCreater = new QuickHullAlgorithm();

        List<Cluster> clusters = new(_plotManager.Clusters);

        var task = Task.Run(() => 
        {
            return clusters.Select(el => hullCreater.CreateHull(el.Points)).ToList();
        });

        var polygons = await task;

        _plotManager.DrawCurrentPolygons(polygons);
    }
}
