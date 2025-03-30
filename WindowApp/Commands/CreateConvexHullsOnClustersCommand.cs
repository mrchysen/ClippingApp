
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
        await Task.Run(() => {
            _plotManager.ClearOnlyPlot();
            _plotManager.Polygons.Clear();
            var hullCreater = new QuickHullAlgorithm();

            foreach (var cluster in _plotManager.Clusters)
            {
                var polygon = hullCreater.CreateHull(cluster.Points);

                _plotManager.DrawCurrentPolygon(polygon, false);
            }
        });
    }
}
