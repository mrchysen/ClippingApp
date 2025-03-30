using WindowApp.Infrastructure;
using WindowApp.SubWindows.ObjectsInfo;

namespace WindowApp.Commands;

public class ShowObjectsInfoCommand : IMainWindowCommand
{
    private PlotManager _plotManager;

    public ShowObjectsInfoCommand(PlotManager plotManager)
    {
        _plotManager = plotManager;
    }

    public Task Handle()
    {
        ObjectsInfoWindow _window = new(_plotManager.Polygons, _plotManager.Points, _plotManager.Clusters);

        _window.Show();

        return Task.CompletedTask;
    }
}
