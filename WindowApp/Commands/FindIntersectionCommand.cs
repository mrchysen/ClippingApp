using WindowApp.Infrastructure;

namespace WindowApp.Commands;

public class FindIntersectionCommand : ICommand
{
    public void Handle(PlotManager plotManager)
    {
        if (plotManager.Polygons.Count > 1)
        {
            plotManager.Polygons.AddRange(plotManager.Clipper.Clip(plotManager.Polygons[0], plotManager.Polygons[1]));
            plotManager.DrawCurrentPolygons();
        }
    }
}
