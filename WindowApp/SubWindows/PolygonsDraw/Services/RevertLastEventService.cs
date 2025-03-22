using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace WindowApp.SubWindows.PolygonsDraw.Services;

public class RevertLastEventService
{
    public void RevertLastEvent(PointsWindowContext context)
    {
        if (context.Points.Count > 0)
        {
            if (context.Points.Count < 3)
            {
                var lastPoint = context.Points.Last();
                var lastEllipse = context.Ellipses.Last();

                if (context.Points.Count == 2)
                {
                    RemoveLastLine(context.Canvas, lastPoint, context.Points[^2]);
                    context.Canvas.Children.Remove(context.Arrows.First());
                    context.Arrows.Clear();
                }
                    
                context.Canvas.Children.Remove(lastEllipse);
                context.Points.Remove(lastPoint);
                context.Ellipses.Remove(lastEllipse);
            }
            else // context.Points >= 3
            {
                var lastPoint = context.Points.Last();
                var lastEllipse = context.Ellipses.Last();
                var lastArrow = context.Arrows.Last();

                RemoveLastLine(context.Canvas, lastPoint, context.Points[^2]);

                context.Points.Remove(lastPoint);
                context.Ellipses.Remove(lastEllipse);
                context.Arrows.Remove(lastArrow);
                context.Canvas.Children.Remove(lastEllipse);
                context.Canvas.Children.Remove(lastArrow);
                context.Canvas.Children.Remove(context.LineBetweenFirstAndEndPoint);
                context.Canvas.Children.Remove(context.PolygonBetweenFirstAndEndPoint);

                if (context.Points.Count > 2)
                {
                    var line = PolygonDraw.CreateLine(context.Points.Last(), context.Points.First());
                    var arrowPoints = PolygonDraw.CreateArrow(context.Points.Last(), context.Points.First());
                    var polygon = PolygonDraw.CreatePolygon(arrowPoints.Item1, arrowPoints.Item2, arrowPoints.Item3);
                    context.LineBetweenFirstAndEndPoint = line;
                    context.PolygonBetweenFirstAndEndPoint = polygon;
                    context.Canvas.Children.Add(line);
                    context.Canvas.Children.Add(polygon);
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