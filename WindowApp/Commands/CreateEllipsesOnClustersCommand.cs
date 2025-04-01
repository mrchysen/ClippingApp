using Core.Clustering;
using Core.HullCreators.SimpleConvexCreators;
using WindowApp.Infrastructure;

namespace WindowApp.Commands;

public class CreateEllipsesOnClustersCommand : IMainWindowCommand
{
    private PlotManager _plotManager;

    public CreateEllipsesOnClustersCommand(PlotManager plotManager)
    {
        _plotManager = plotManager;
    }

    public async Task Handle()
    {
        _plotManager.ClearOnlyPlot();
        _plotManager.Polygons.Clear();

        List<Cluster> clusters = new(_plotManager.Clusters);

        var task = Task.Run(() =>
        {
            return clusters.Where(el => el.Points.Count > 0).Select(el =>
            {
                var hullCreator = new EllipseHullCreator(el.Centroid);
                return hullCreator.CreateHull(el.Points);
            }).ToList();
        });

        var polygons = await task;

        _plotManager.DrawCurrentPolygons(polygons, true);
    }
}
