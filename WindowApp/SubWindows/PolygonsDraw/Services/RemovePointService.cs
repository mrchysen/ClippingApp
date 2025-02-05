using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace WindowApp.SubWindows.PolygonsDraw.Services;

public class RemovePointService
{
    public void RemovePoint(PolygonDrawContext context)
    {
        if (context.Points.Count > 0)
        {
            if (context.Points.Count < 3)
            {
                var lastPoint = context.Points.Last();
                var lastEllipse = context.Ellipses.Last();

                if (context.Points.Count == 2)
                    RemoveLastLine(context.Canvas, lastPoint, context.Points[^2]);

                context.Canvas.Children.Remove(lastEllipse);
                context.Points.Remove(lastPoint);
                context.Ellipses.Remove(lastEllipse);
            }
            else // context.Points >= 3
            {
                var lastPoint = context.Points.Last();
                var lastEllipse = context.Ellipses.Last();

                RemoveLastLine(context.Canvas, lastPoint, context.Points[^2]);

                context.Canvas.Children.Remove(lastEllipse);
                context.Points.Remove(lastPoint);
                context.Ellipses.Remove(lastEllipse);
                context.Canvas.Children.Remove(context.LineBetwenFirstAndEndPoint);

                if (context.Points.Count > 2)
                {
                    var line = PolygonDraw.CreateLine(context.Points.Last(), context.Points.First());
                    context.LineBetwenFirstAndEndPoint = line;
                    context.Canvas.Children.Add(line);
                }
            }
        }
    }

    public void RemoveLastLine(Canvas canvas, Point p1, Point p2)
    {
        foreach (var children in canvas.Children)
        {
            if (children is Line line)
            {
                if (line.X1 == p1.X && line.Y1 == p1.Y && line.X2 == p2.X && line.Y2 == p2.Y ||
                   line.X1 == p2.X && line.Y1 == p2.Y && line.X2 == p1.X && line.Y2 == p1.Y)
                {
                    canvas.Children.Remove(line);
                    break;
                }
            }
        }
    }
}