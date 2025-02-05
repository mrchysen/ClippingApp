using Application.PlotExtensions;
using Application.PolygonPlotting;
using Core.HullCreators.QuickHull;
using Core.Models.Points;
using Core.Models.Polygons;
using System.Diagnostics;
using WindowApp.Infrastructure;

namespace WindowApp.Commands;

public class CreateRandomHullCommand : ICommand
{
    private readonly IPolygonGenerator _polygonGenerator;

    public CreateRandomHullCommand(IPolygonGenerator polygonGenerator)
    {
        _polygonGenerator = polygonGenerator;
    }

    public void Handle(PlotManager plotManager)
    {
        var quickHull = new QuickHullAlgorithm();

        List<PointD> points = _polygonGenerator.GeneratePoints(6);

        //List<PointD> points = new List<PointD>
        //{
        //    new (5.3836, 8.227),
        //    new (8.1125, 2.8715),
        //    new (9.9417, 2.5185),
        //    new (2.1713, 13.3227),
        //    new (5.3049, 1.1093),
        //    new (13.1928, 11.0976)
        //};

        var polygon = quickHull.CreateHull(points);

        plotManager.Plot.Clear();

        IPolygonArtist artist = new PolygonArtist([polygon]);

        artist.Plot(plotManager.Plot, true);
        plotManager.Plot.Axes.AutoScale();
        plotManager.Plot.AddMarkersWithNumbers(points);

        DebugPrintInfo(points);

        plotManager.WpfPlot.Refresh();
    }

    private void DebugPrintInfo(List<PointD> list)
    {
        Debug.WriteLine($"--- list start ---");
        var c = 0;
        foreach (var item in list)
        {
            if (c == 3)
            {
                c = 0;
                Debug.Write("\n");
            }
            Debug.Write($"{item} ->> ");
            c++;
        }
        Debug.Write("\n");
        Debug.WriteLine($"--- list end ---");
    }
}
