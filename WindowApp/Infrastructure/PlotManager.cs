using Application.PlotExtensions;
using Application.PolygonPlotting;
using Core.Clippers;
using Core.Clustering;
using Core.Colors;
using Core.Models.Points;
using Core.Models.Polygons;
using ScottPlot;
using ScottPlot.WPF;

namespace WindowApp.Infrastructure;

// TODO: сильно толстый класс => распилить на маленькие вынести в сервисы
public class PlotManager
{
    private WpfPlot _wpfPlot;
    private List<Polygon> _polygons;
    private List<PointD> _points;
    private List<Cluster> _clusters;

    public PlotManager(
        WpfPlot wpfPlot,
        List<Polygon>? polygons = null, 
        List<PointD>? points = null,
        List<Cluster>? cluster = null)
    {
        _wpfPlot = wpfPlot;
        _polygons = polygons ?? new();
        _points = points ?? new();
        _clusters = cluster ?? new();
    }

    public Plot Plot => _wpfPlot.Plot;

    public WpfPlot WpfPlot => _wpfPlot;

    public List<Polygon> Polygons => _polygons;

    public List<PointD> Points => _points;

    public List<Cluster> Clusters => _clusters;

    public void DrawCurrentPolygons(
        List<Polygon> polygons, 
        bool drawClustersPoints = false,
        bool ClearLastPolygons = true)
    {
        _polygons = ClearLastPolygons ? [..polygons] : [.._polygons, ..polygons];

        IPolygonArtist artist = new PolygonArtist(_polygons);

        Plot.Clear();
        artist.Draw(Plot, true);
        Plot.Axes.AutoScale();

        WpfPlot.Refresh();

        if (drawClustersPoints)
            DrawClusters();
    }

    public void DrawCurrentPolygon(Polygon polygon, bool ClearLastPolygons = true)
    {
        _polygons = ClearLastPolygons ? [polygon] : [polygon, .. _polygons];
        IPolygonArtist artist = new PolygonArtist(_polygons);

        if (ClearLastPolygons)
        {
            Plot.Clear();
        }
        artist.Draw(Plot, true);
        Plot.Axes.AutoScale();

        WpfPlot.Refresh();
    }

    public void DrawCurrentPoints(List<PointD> points)
    {
        _points = points;

        Plot.Clear();

        foreach (var point in points)
        {
            Plot.Add.Marker(point.X, point.Y, size: 10, color:Colors.Black);
        }

        Plot.Axes.AutoScale();

        WpfPlot.Refresh();
    }

    public void ClearOnlyPlot()
    {
        _wpfPlot.Plot.Clear();
        _wpfPlot.Refresh();
    }

    public void DrawClusters()
    {
        var colors = Enumerable.Range(0, _clusters.Count)
            .Select(el => RandomColor.Get())
            .ToList();

        for (int i = 0; i < _clusters.Count; i++)
        {
            Plot.AddMarkers(_clusters[i].Points,
                colors[i],
                14);

            Plot.AddOneMarker(_clusters[i].Centroid,
                colors[i],
                MarkerShape.Eks,
                16);
        }

        Plot.Axes.AutoScale();
        WpfPlot.Refresh();
    }

    public void Clear()
    {
        _points.Clear();
        _polygons.Clear();
        _clusters.Clear();
        _wpfPlot.Plot.Clear();
        _wpfPlot.Refresh();
    }
}