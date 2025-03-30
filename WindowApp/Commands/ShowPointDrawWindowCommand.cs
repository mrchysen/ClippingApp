using WindowApp.Infrastructure;
using WindowApp.SubWindows.PointsDraw;

namespace WindowApp.Commands;

public class ShowPointDrawWindowCommand : IMainWindowCommand
{
    private PlotManager _plotManager;

    public ShowPointDrawWindowCommand(PlotManager plotManager)
    {
        _plotManager = plotManager;
    }

    public Task Handle()
    {
        var window = new PointsWindow();

        window.ShowDialog();

        var context = window.Context;

        if (context.Points.Count > 0)
        {
            _plotManager.DrawCurrentPoints(context.Points);
        }

        return Task.CompletedTask;
    }
}
