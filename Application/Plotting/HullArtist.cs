using Application.Converters.Polygons;
using Application.PlotExtensions;
using Core.Models.Points;
using ScottPlot;

namespace Application.PolygonPlotting;

public class HullArtist : IPlotArtist
{
    private Core.Models.Polygons.Polygon _hull;
    private List<PointD> _pointsInsideHull;
    
    public int PointSize { get; set; } = 12;

    public HullArtist(
        Core.Models.Polygons.Polygon hull, 
        List<PointD> pointsInsideHull)
    {
        _hull = hull;
        _pointsInsideHull = pointsInsideHull;
    }

    public Plot Draw(Plot? plotInput)
    {
        var plot = plotInput ?? new();

        var hullAsPolygon = new PolygonConverter().ConvertToScottPlot(_hull);

        plot.PlottableList.Add(hullAsPolygon);

        plot.AddPolygonMarkersWithNumbers(
            _hull, 
            size: 14,
            coreColor: _hull.Color,
            configureMarker: (m) => {
                m.LineWidth = 1;
                m.MarkerLineColor = new(0, 0, 0);
                },
            delta: 5);

        plot.AddMarkers(
            _pointsInsideHull,
            size: 14);

        return plot;
    }
}
