using WindowApp.Infrastructure;

namespace WindowApp.KeyPressedHandler.Handlers;

// F = Full screen polygon
public class KeyFHandler : KeyHandler
{
    public override void Handle(PlotManager plotManager)
    {
        plotManager.Plot.Axes.AutoScale();

        plotManager.WpfPlot.Refresh();
    }
}
