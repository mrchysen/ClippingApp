namespace WindowApp.KeyPressedHandler.Handlers;

// F = Full screen polygon
public class KeyFHandler : KeyHandler
{
    public override Task Handle(KeyHandlerObject obj)
    {
        obj.Plot.Axes.AutoScale();

        obj.UiPlot.Refresh();

        return Task.CompletedTask;
    }
}
