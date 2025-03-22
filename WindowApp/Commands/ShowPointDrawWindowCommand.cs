using WindowApp.Infrastructure;
using WindowApp.SubWindows.PointsDraw;

namespace WindowApp.Commands;

public class ShowPointDrawWindowCommand : ICommand
{
    public Task Handle(PlotManager plotManager)
    {
        var window = new PointsWindow();

        window.ShowDialog();

        var context = window.Context;

        if (context.Points.Count > 0)
        {
            plotManager.DrawCurrentPoints(context.Points);
        }

        return Task.CompletedTask;
    }
}
