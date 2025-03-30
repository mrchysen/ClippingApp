namespace WindowApp.SubWindows.PointsDraw.PointsDrawCommands;

public class RemoveAllPointsCommand : IPointsDrawCommand
{
    public void Handle(PointsWindowViewModel context)
    {
        context.Canvas.Children.Clear();
        context.Ellipses.Clear();
        context.Points.Clear();
    }
}
