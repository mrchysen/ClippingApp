using Core.Clippers;
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
        if (_plotManager.Polygons.Count > 1)
        {
            _plotManager.Polygons.AddRange(
                _clipper.Clip(
                    _plotManager.Polygons[0],
                    _plotManager.Polygons[1]));
            _plotManager.DrawCurrentPolygons();
        }

        return Task.CompletedTask;
    }
}
