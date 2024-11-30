using WindowApp.Infrastructure;

namespace WindowApp.KeyPressedHandler;

public abstract class KeyHandler
{
    public abstract void Handle(PlotManager plotManager);
}
