using WindowApp.SubWindows.PolygonsDraw.Services;

namespace WindowApp.SubWindows.PolygonsDraw.PolygonDrawCommands;

public class ClearPolygonCommand : IDrawPolygonCommand
{
    public void Handle(PointsWindowContext context)
    {
        var service = new RevertLastEventService();

        while (context.Points.Count > 0)
        {
            service.RevertLastEvent(context);
        }
    }
}
