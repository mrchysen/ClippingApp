using Application.PolygonPlotting;
using Core.Clippers;
using Core.Models.Points;
using Core.Models.Polygons;
using ScottPlot;
using ScottPlot.WPF;

namespace WindowApp.Infrastructure;

public class PlotManager
{
    private WpfPlot _wpfPlot;
    private List<Polygon> _polygons;
    private List<PointD> _points; 

    public PlotManager(
        WpfPlot wpfPlot, 
        IClipper clipper, 
        MainWindowContext context, 
        List<Polygon>? polygons = null, 
        List<PointD>? points = null)
    {
        _wpfPlot = wpfPlot;
        Clipper = clipper;
        MainWindowContext = context;
        _polygons = polygons ?? new();
        _points = points ?? new();
    }

    public Plot Plot => _wpfPlot.Plot;

    public WpfPlot WpfPlot => _wpfPlot;

    public List<Polygon> Polygons => _polygons;

    public List<PointD> Points => _points;

    public IClipper Clipper { get; set; }

    public MainWindowContext MainWindowContext { get; set; }

    public void DrawCurrentPolygons()
    {
        IPolygonArtist artist = new PolygonArtist(Polygons);

        Plot.Clear();
        artist.Plot(Plot, true);
        Plot.Axes.AutoScale();

        WpfPlot.Refresh();
    }

    public void DrawCurrentPolygons(Polygon polygon)
    {
        _polygons = [polygon];
        IPolygonArtist artist = new PolygonArtist([polygon]);

        Plot.Clear();
        artist.Plot(Plot, true);
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
}