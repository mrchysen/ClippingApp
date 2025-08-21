using Core.Colors;
using Core.Models.Colors;
using Core.Models.Points;
using ScottPlot;
using ScottPlot.Plottables;

namespace Application.PlotExtensions;

public static class PlotMarkerExtension
{
    public static void AddMarker(this Plot plot, 
        PointD point, 
        CoreColor? color = null, 
        MarkerShape shape = MarkerShape.FilledCircle,
        int size = 6)
    {
        var pointColor = color ?? RandomColor.Get();

        plot.Add.Marker(
            point.X, 
            point.Y,
            shape,
            color: new(
                pointColor.R,
                pointColor.G,
                pointColor.B),
            size: size);
    }

    public static void AddMarkers(this Plot plot, List<PointD> points, CoreColor? color = null, int size = 6)
    {
        var pointColor = color ?? RandomColor.Get();

        for (int i = 0; i < points.Count; i++)
        {
            var p = points[i];

            plot.AddMarker(p, pointColor, size: size);
        }
    }

    public static void AddMarkersWithNumbers(this Plot plot, List<PointD> points)
    {
        var color = RandomColor.Get();

        for (int i = 0; i < points.Count; i++)
        {
            var p = points[i];

            plot.Add.Marker(p.X, p.Y, color: new Color(0, 0, 0), size: 6);
            plot.Add.Text((i + 1).ToString(), new Coordinates(p.X, p.Y));
        }
    }

    public static void AddPolygonMarkersWithNumbers(
        this Plot plot,
        Core.Models.Polygons.Polygon polygon,
        CoreColor? coreColor = null,
        int size = 8,
        double delta = 0.125d,
        Action<Marker>? configureMarker = null)
    {
        var color = coreColor ?? RandomColor.Get();

        for (int i = 0; i < polygon.Count; i++)
        {
            var p = polygon.Points[i];

            var marker = plot.Add.Marker(
                p.X, 
                p.Y, 
                color: new Color(color.R, color.G, color.B), 
                size: size);

            configureMarker?.Invoke(marker);

            var text = plot.Add.Text((i + 1).ToString(), new Coordinates(p.X, p.Y));

            text.LabelFontSize = 18;
            text.LabelOffsetX = (float)delta;
            text.LabelOffsetY = (float)delta;
        }
    }

    public static void AddMarkersWithNumbers(
        this Plot plot, 
        List<Core.Models.Polygons.Polygon> polygons)
    {
        foreach (var polygon in polygons) 
        { 
            plot.AddPolygonMarkersWithNumbers(polygon); 
        }
    }
}
