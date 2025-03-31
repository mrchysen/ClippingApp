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
        await Task.Run(() => {

            lock (_plotManager.Clusters)
            {
                _plotManager.ClearOnlyPlot();
                _plotManager.Polygons.Clear();
            }
            var hullCreater = new QuickHullAlgorithm();

            List<Cluster> clusters = new(_plotManager.Clusters);
            

            for (int i = 0; i < clusters.Count; i++)
            {
                var polygon = hullCreater.CreateHull(clusters[i].Points);

                _plotManager.DrawCurrentPolygon(polygon, false);
            }
        });
    }
}
