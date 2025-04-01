using Application.Randoms;
using System.Windows.Controls;

namespace WindowApp.SubWindows.PointsDraw.PointsDrawCommands;

public class GenerateRandomPointsCommand : IPointsDrawCommand
{
    public void Handle(PointsWindowViewModel context)
    {
        var width = context.Canvas.ActualWidth;
        var height = context.Canvas.ActualHeight;

        var pointGenerator = new RandomPolygonGenerator(new()
        {
            Area = new()
            {
                Height = (int)height,
                Width = (int)width,
                X = 0,
                Y = 0
            } 
        });

        var points = pointGenerator.GeneratePoints(context.PointCount);

        var ellipses = points.Select(p => (p, PointsWindow.CreateEllipse()));

        foreach(var ellipse in ellipses)
        {
            context.Points.Add(new(ellipse.p.X / PointsWindow.DeltaX, -ellipse.p.Y / PointsWindow.DeltaX));
            context.Ellipses.Add(ellipse.Item2);

            Canvas.SetLeft(ellipse.Item2, ellipse.p.X);
            Canvas.SetTop(ellipse.Item2, ellipse.p.Y);

            context.Canvas.Children.Add(ellipse.Item2);
        }
    }
}
