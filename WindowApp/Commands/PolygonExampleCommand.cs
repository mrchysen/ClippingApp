
using Application.PolygonPlotting;
using Core.Clippers.WeilerAthertonPolygonClipper;
using Core.Clippers;
using WindowApp.Infrastructure;
using Core.Models.Polygons;
using Application.Randoms;

namespace WindowApp.Commands;

public class PolygonExampleCommand : IMainWindowCommand
{
    private PlotManager _plotManager;
    private readonly IPolygonGenerator _polygonGenerator;

    public PolygonExampleCommand(PlotManager plotManager, IPolygonGenerator? polygonGenerator = null)
    {
        _plotManager = plotManager;
        _polygonGenerator = polygonGenerator ?? new RandomPolygonGenerator();
    }

    public Task Handle()
    {
        _plotManager.Polygons.Clear();
        _plotManager.Polygons.AddRange([
            _polygonGenerator.Generate(true, IsClokwiseAlgotithm(_plotManager.Clipper), count:5),
            _polygonGenerator.Generate(true, IsClokwiseAlgotithm(_plotManager.Clipper), count:3)
        ]);
        _plotManager.Polygons.AddRange(_plotManager.Clipper.Clip(_plotManager.Polygons[0], _plotManager.Polygons[1]));
        IPolygonArtist artist = new PolygonArtist(_plotManager.Polygons);
        _plotManager.Plot.Clear();
        artist.Draw(_plotManager.Plot, true);
        _plotManager.Plot.Axes.AutoScale();

        _plotManager.WpfPlot.Refresh();

        return Task.CompletedTask;
    }

    private bool IsClokwiseAlgotithm(IClipper clipper)
    {
        if (clipper is WeilerAthertonPolygonClipper)
            return true;

        return false;
    }
}
