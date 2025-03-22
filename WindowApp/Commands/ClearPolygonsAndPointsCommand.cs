using WindowApp.Infrastructure;

namespace WindowApp.Commands;

public class ClearPolygonsAndPointsCommand : ICommand
{
    public Task Handle(PlotManager plotManager)
    {
        plotManager.Points.Clear();
        plotManager.Polygons.Clear();

        plotManager.DrawCurrentPolygons();

        return Task.CompletedTask;
    }
}
