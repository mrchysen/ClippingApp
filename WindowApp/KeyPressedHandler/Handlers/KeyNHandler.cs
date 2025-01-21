using Application.PlotExtensions;
using Application.PolygonPlotting;
using Application.Randoms;
using Core.Clippers;
using Core.Clippers.ConvexPolygonClipper;
using Core.Models.Polygons;
using WindowApp.Infrastructure;

namespace WindowApp.KeyPressedHandler.Handlers;

// N = next polygons intersection
public class KeyNHandler : KeyHandler
{
    private readonly RandomPolygon _randomPolygon;

    public KeyNHandler(RandomPolygon randomPolygon)
    {
        _randomPolygon = randomPolygon;
    }

    public override void Handle(PlotManager plotManager)
    {
        plotManager.Polygons.Clear();
        plotManager.Polygons.AddRange([
            _randomPolygon.Get(true, 5),
            _randomPolygon.Get(true, 3)
            ]);

        plotManager.Polygons.AddRange(plotManager.Clipper.Clip(plotManager.Polygons[0], plotManager.Polygons[1]));
        
        IPolygonArtist artist = new PolygonArtist(plotManager.Polygons);

        plotManager.Plot.Clear();
        artist.Plot(plotManager.Plot);
        plotManager.Plot.Axes.AutoScale();

        plotManager.WpfPlot.Refresh();
    }
}
