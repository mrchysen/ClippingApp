using Application.PolygonPlotting;
using Core.Clustering;
using Core.Models.Polygons;
using ScottPlot;

namespace Application.Plotting;

public class ClippedClustersArtist : IPlotArtist
{
    private List<Cluster> _clusters;
    private List<Polygon> _clippedClusters;

    public ClippedClustersArtist(
        List<Cluster> clusters, 
        List<Polygon> clippedClusters)
    {
        _clusters = clusters;
        _clippedClusters = clippedClusters;
    }

    public Plot Draw(Plot? plotInput)
    {
        var plot = plotInput ?? new Plot();

        return plot;
    }
}
