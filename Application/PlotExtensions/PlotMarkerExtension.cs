using Core.Colors;
using Core.Models.Colors;
using Core.Models.Points;
using Core.Models.Polygons;
using ScottPlot;
using System.Diagnostics;

namespace Application.PlotExtensions;

public static class PlotMarkerExtension
{
    public static void AddOneMarker(this Plot plot, 
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

            plot.AddOneMarker(p, pointColor, size: size);
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

    public static void AddMarkersWithNumbers(this Plot plot, Polygon polygon)
    {
        var color = RandomColor.Get();

        for (int i = 0; i < polygon.Count; i++)
        {
            var p = polygon.Points[i];

            plot.Add.Marker(p.X, p.Y, color: new Color(0, 0, 0), size: 6);
            plot.Add.Text((i + 1).ToString(), new Coordinates(p.X, p.Y));
        }
    }

    public static void AddMarkersWithNumbers(this Plot plot, List<Polygon> polygons)
    {
        foreach (var polygon in polygons) 
        { 
            plot.AddMarkersWithNumbers(polygon); 
        }
    }
}
