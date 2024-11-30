using Application.PolygonPlotting;
using Application.Randoms;
using Core.Clippers;
using Core.Clippers.ConvexPolygonClipper;
using Core.Models.Polygons;
using WindowApp.Infrastructure;

namespace WindowApp.KeyPressedHandler.Handlers;

// N = next
public class KeyNHandler : KeyHandler
{
    private readonly RandomPolygon _randomPolygon;
    private readonly List<Polygon> _polygons;
    private readonly IClipper _clipper;

    public KeyNHandler(
        RandomPolygon randomPolygon,
        List<Polygon> polygons,
        ConvexPolygonClipper clipper)
    {
        _randomPolygon = randomPolygon;
        _polygons = polygons;
        _clipper = clipper;
    }

    public override void Handle(PlotManager plotManager)
    {
        _polygons.Clear();
        _polygons.AddRange([
            _randomPolygon.Get(true, 5),
            _randomPolygon.Get(true, 3)
            ]);

        _polygons.AddRange(_clipper.Clip(_polygons));

        IPolygonArtist artist = new PolygonArtist(_polygons);

        plotManager.Plot.Clear();
        artist.Plot(plotManager.Plot);
        plotManager.Plot.Axes.AutoScale();

        plotManager.WpfPlot.Refresh();
    }
}
