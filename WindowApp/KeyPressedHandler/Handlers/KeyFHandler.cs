namespace WindowApp.KeyPressedHandler.Handlers;

// F = Full screen polygon
public class KeyFHandler : KeyHandler
{
    public override void Handle(KeyHandlerObject obj)
    {
        obj.Plot.Axes.AutoScale();

        obj.UiPlot.Refresh();
    }
}
