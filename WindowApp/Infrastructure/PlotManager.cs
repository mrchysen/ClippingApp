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
    
    public PlotManager(WpfPlot wpfPlot, List<Polygon> polygons, IClipper clipper)
    {
        _polygons = polygons;
        _wpfPlot = wpfPlot;
        Clipper = clipper;
    }

    public Plot Plot => _wpfPlot.Plot;

    public WpfPlot WpfPlot => _wpfPlot;

    public List<Polygon> Polygons => _polygons;

    public IClipper Clipper { get; set; }
}