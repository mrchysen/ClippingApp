namespace WindowApp.SubWindows.PointsDraw.PointsDrawCommands;

public interface IPointsDrawCommand
{
    void Handle(PointsWindowContext context);
}
