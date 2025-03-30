using WindowApp.Infrastructure;

namespace WindowApp.Commands;

public class FindIntersectionCommand : IMainWindowCommand
{
    private PlotManager _plotManager;

    public FindIntersectionCommand(PlotManager plotManager)
    {
        _plotManager = plotManager;
    }

    public Task Handle()
    {
        if (_plotManager.Polygons.Count > 1)
        {
            _plotManager.Polygons.AddRange(_plotManager.Clipper.Clip(_plotManager.Polygons[0], _plotManager.Polygons[1]));
            _plotManager.DrawCurrentPolygons();
        }

        return Task.CompletedTask;
    }
}
