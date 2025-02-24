using System.Windows;
using WindowApp.Infrastructure;
using WindowApp.SubWindows.PolygonsDraw;

namespace WindowApp.Commands;

public class ShowPolygonDrawWindowCommand : ICommand
{
    public void Handle(PlotManager plotManager)
    {
        var window = new PolygonDraw();

        var result = window.ShowDialog();

        MessageBox.Show(result.ToString());

        var context = window.PolygonDrawContext;

        if(context.Polygons.Count > 0)
        {
            plotManager.Polygons.Clear();
            plotManager.Polygons.AddRange(context.Polygons);
            plotManager.DrawCurrentPolygons();
        }
    }
}
