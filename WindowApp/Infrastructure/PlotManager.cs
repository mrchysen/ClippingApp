using Application.PolygonPlotting;
using Core.Clippers;
using Core.Clustering;
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
        IClipper clipper, 
        MainWindowViewModel context, 
        List<Polygon>? polygons = null, 
        List<PointD>? points = null,
        List<Cluster>? cluster = null)
    {
        _wpfPlot = wpfPlot;
        Clipper = clipper;
        MainWindowContext = context;
        _polygons = polygons ?? new();
        _points = points ?? new();
        _clusters = cluster ?? new();
    }

    public Plot Plot => _wpfPlot.Plot;

    public WpfPlot WpfPlot => _wpfPlot;

    public List<Polygon> Polygons => _polygons;

    public List<PointD> Points => _points;

    public List<Cluster> Clusters => _clusters;

    public IClipper Clipper { get; set; }

    public MainWindowViewModel MainWindowContext { get; set; }

    public void DrawCurrentPolygons()
    {
        IPolygonArtist artist = new PolygonArtist(Polygons);

        Plot.Clear();
        artist.Draw(Plot, true);
        Plot.Axes.AutoScale();

        WpfPlot.Refresh();
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

    public void Clear()
    {
        _points.Clear();
        _polygons.Clear();
        _clusters.Clear();
        _wpfPlot.Plot.Clear();
        _wpfPlot.Refresh();
    }
}