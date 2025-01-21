using Core.Colors;
using Core.Models.Polygons;
using ScottPlot;

namespace Application.PlotExtensions;

public static class PlotMarkerExtension
{
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
