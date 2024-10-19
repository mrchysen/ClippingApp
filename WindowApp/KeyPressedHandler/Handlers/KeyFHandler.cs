namespace WindowApp.KeyPressedHandler.Handlers;

public class KeyFHandler : KeyHandler
{
    public override void Handle(KeyHandlerObject obj)
    {
        obj.Plot.Axes.AutoScale();

        obj.UiPlot.Refresh();
    }
}
