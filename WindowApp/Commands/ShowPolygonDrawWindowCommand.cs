using WindowApp.Infrastructure;
using WindowApp.SubWindows.PolygonsDraw;

namespace WindowApp.Commands;

public class ShowPolygonDrawWindowCommand : IMainWindowCommand
{
    private PlotManager _plotManager;

    public ShowPolygonDrawWindowCommand(PlotManager plotManager)
    {
        _plotManager = plotManager;
    }

    public Task Handle()
    {
        var window = new PolygonDraw();

        window.ShowDialog();

        var context = window.PolygonDrawContext;

        if(context.Polygons.Count > 0)
        {
            _plotManager.DrawCurrentPolygons(context.Polygons);
        }

        return Task.CompletedTask;
    }
}
