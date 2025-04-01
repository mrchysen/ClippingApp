namespace WindowApp.SubWindows.PointsDraw.PointsDrawCommands;

public class RevertLastAddCommand : IPointsDrawCommand
{
    public void Handle(PointsWindowViewModel context)
    {
        if (context.Points.Count == 0)
            return;

        var lastEllipse = context.Ellipses.Last();

        context.Canvas.Children.Remove(lastEllipse);

        context.Ellipses.Remove(lastEllipse);
        context.Points.Remove(context.Points.Last());
    }
}
