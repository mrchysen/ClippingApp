using Core.Clippers;
using Core.Clippers.BulkIntersections;
using Core.Models.Polygons;
using WindowApp.Infrastructure;

namespace WindowApp.Commands;

public class FindIntersectionCommand : IMainWindowCommand
{
    private PlotManager _plotManager;
    private IClipper _clipper;

    public FindIntersectionCommand(
        PlotManager plotManager,
        IClipper clipper)
    {
        _plotManager = plotManager;
        _clipper = clipper;
    }

    public Task Handle()
    {
        if (_plotManager.Polygons.Count < 1)
            return Task.CompletedTask;

        List<Polygon> polygons = new();

        if(_plotManager.Polygons.Count == 2)
        {
            polygons =
                _clipper.Clip(
                        _plotManager.Polygons[0],
                        _plotManager.Polygons[1])
                .Where(p => p.Points.Count > 0)
                .ToList();
        }
        else
        {
            polygons = new BulkIntersection(_clipper)
                .FindAllClips(_plotManager.Polygons)
                .Where(p => p.Count > 0)
                .ToList();
        }

        _plotManager.DrawCurrentPolygons(polygons, ClearLastPolygons: false);

        return Task.CompletedTask;
    }
}
