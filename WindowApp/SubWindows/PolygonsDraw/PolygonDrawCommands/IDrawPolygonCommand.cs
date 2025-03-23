namespace WindowApp.SubWindows.PolygonsDraw.PolygonDrawCommands
{
    public interface IDrawPolygonCommand
    {
        void Handle(PointsWindowContext context);
    }
}