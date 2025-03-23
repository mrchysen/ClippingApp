using Core.PointsOrderers;
using OpenTK.Graphics.ES11;
using WindowApp.Infrastructure;
using WindowApp.SubWindows.Polygons;

namespace WindowApp.KeyPressedHandler.Handlers;

// I = Info
public class KeyIHandler : KeyHandler
{
    private PolygonsWindow _window = null!;

    public KeyIHandler() { }

    public override void Handle(PlotManager plotManager)
    {
        _window = new(plotManager.Polygons, plotManager.Points, plotManager.Clusters);

        _window.Show();
    }
}
