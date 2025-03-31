
using Core.Clustering;
using Core.HullCreators.QuickHull;
using Core.HullCreators.SimpleConvexCreators;
using System.Linq;
using System.Windows.Shapes;
using WindowApp.Infrastructure;

namespace WindowApp.Commands;

public class CreateShapeOfClustersCommand : IMainWindowCommand
{
    private PlotManager _plotManager;
    // Можно добавить интерфейс для создания оболочки

    public CreateShapeOfClustersCommand(PlotManager plotManager)
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
