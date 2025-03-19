using Application.PolygonPlotting;
using Core.Clippers;
using Core.Clippers.ConvexPolygonClipper;
using Core.Models.Polygons;
using ScottPlot;
using ScottPlot.WPF;

namespace WindowApp.Infrastructure;

public class PlotManager
{
    private WpfPlot _wpfPlot;
    private List<Polygon> _polygons;
    
    public PlotManager(WpfPlot wpfPlot, List<Polygon> polygons, IClipper clipper, MainWindowContext context)
    {
        _polygons = polygons;
        _wpfPlot = wpfPlot;
        Clipper = clipper;
        MainWindowContext = context;
    }

    public Plot Plot => _wpfPlot.Plot;

    public WpfPlot WpfPlot => _wpfPlot;

    public List<Polygon> Polygons => _polygons;

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
}