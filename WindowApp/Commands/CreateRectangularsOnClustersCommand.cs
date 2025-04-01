using Core.Clustering;
using Core.HullCreators.SimpleConvexCreators;
using WindowApp.Infrastructure;

namespace WindowApp.Commands;

public class CreateRectangularsOnClustersCommand : IMainWindowCommand
{
    private PlotManager _plotManager;

    public CreateRectangularsOnClustersCommand(PlotManager plotManager)
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
                var hullCreator = new RectangularHullCreator(el.Centroid);
                return hullCreator.CreateHull(el.Points);
            }).ToList();
        });
        //<3
        var polygons = await task;

        _plotManager.DrawCurrentPolygons(polygons, true);
    }
}
