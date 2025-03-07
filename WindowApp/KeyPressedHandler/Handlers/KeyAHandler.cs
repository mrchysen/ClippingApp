using Application.PolygonPlotting;
using Core.Clippers.WeilerAthertonPolygonClipper;
using Core.HullCreators.NoncovexAlgorithms;
using Core.Models.Points;
using Core.Models.Polygons;
using WindowApp.Infrastructure;

namespace WindowApp.KeyPressedHandler.Handlers;

// A key = some activity
public class KeyAHandler : KeyHandler
{
    private readonly WeilerAthertonPolygonClipper clipper;

    public KeyAHandler(WeilerAthertonPolygonClipper clipper)
    {
        this.clipper = clipper;
    }

    public override void Handle(PlotManager plotManager)
    {
        var pol1 = new List<PointD>([
            new(0,0),
            new(10,0),
            new(5,2),
            new(5,1),
            ]);

        new PoogachevAlgorithm(5).CreateHull(pol1);
    }
}
