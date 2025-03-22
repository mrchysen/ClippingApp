using System.Windows;
using WindowApp.Infrastructure;
using WindowApp.SubWindows.PolygonsDraw;

namespace WindowApp.Commands;

public class ShowPolygonDrawWindowCommand : ICommand
{
    public Task Handle(PlotManager plotManager)
    {
        var window = new PolygonDraw();

        window.ShowDialog();

        var context = window.PolygonDrawContext;

        if(context.Polygons.Count > 0)
        {
            plotManager.Polygons.Clear();
            plotManager.Polygons.AddRange(context.Polygons);
            plotManager.DrawCurrentPolygons();
        }

        return Task.CompletedTask;
    }
}
