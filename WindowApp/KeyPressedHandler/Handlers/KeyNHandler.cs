using Application.PolygonPlotting;
using Application.Randoms;
using Core.Clippers;
using Core.Clippers.WeilerAthertonPolygonClipper;
using Core.Models.Polygons;
using WindowApp.Infrastructure;

namespace WindowApp.KeyPressedHandler.Handlers;

// N = next polygons intersection
public class KeyNHandler : KeyHandler
{
    private readonly IPolygonGenerator _randomPolygon;

    public KeyNHandler(IPolygonGenerator randomPolygon)
    {
        _randomPolygon = randomPolygon;
    }

    public override void Handle(PlotManager plotManager)
    {
        plotManager.Polygons.Clear();
        plotManager.Polygons.AddRange([
            _randomPolygon.Generate(true, IsClokwiseAlgotithm(plotManager.Clipper), count:5),
            _randomPolygon.Generate(true, IsClokwiseAlgotithm(plotManager.Clipper), count:3)
            ]);

        plotManager.Polygons.AddRange(plotManager.Clipper.Clip(plotManager.Polygons[0], plotManager.Polygons[1]));
        
        IPolygonArtist artist = new PolygonArtist(plotManager.Polygons);

        plotManager.Plot.Clear();
        artist.Plot(plotManager.Plot, true);
        plotManager.Plot.Axes.AutoScale();

        plotManager.WpfPlot.Refresh();
    }

    private bool IsClokwiseAlgotithm(IClipper clipper)
    {
        if(clipper is WeilerAthertonPolygonClipper)
            return true;

        return false;
    }
}
