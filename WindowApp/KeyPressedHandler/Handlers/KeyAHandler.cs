using Application.PolygonPlotting;
using Core.Clippers.WeilerAthertonPolygonClipper;
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
        var pol1 = new Polygon([
            new(1,1),
            new(1,10),
            new(10,1)
            ]);

        //var pol1 = new Polygon([
        //    new(1,1),
        //    new(1,10),
        //    new(10,10),
        //    new(10,1)
        //    ]);

        //var pol2 = new Polygon([
        //    new(2,2),
        //    new(3,9),
        //    new(9,3)
        //    ]);

        var pol2 = new Polygon([
            new(2,5),
            new(10,20),
            new(20,10),
            new(5,2),
            new(10,10),
            ]);

        //var pol2 = new Polygon([
        //    new(-1,4),
        //    new(12,6),
        //    new(-1,8),
        //    new(14,10),
        //    new(14,2),
        //    ]);

        IPolygonArtist artist = new PolygonArtist([pol1, pol2, ..clipper.Clip(pol1, pol2)]);
        
        plotManager.Plot.Clear();
        artist.Plot(plotManager.Plot, true);



    }
}
