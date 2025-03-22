using WindowApp.SubWindows.PolygonsDraw.Services;

namespace WindowApp.SubWindows.PolygonsDraw.PolygonDrawCommands;

public class RevertLastAddCommand : IDrawPolygonCommand
{
    public void Handle(PointsWindowContext context) 
        => new RevertLastEventService().RevertLastEvent(context);
}
