using Core.Colors;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using WindowApp.Extensions;

namespace WindowApp.SubWindows.PolygonsDraw;

public class PolygonDrawContext
{
    public List<Point> Points { get; set; } = new();
    public List<Ellipse> Ellipses { get; set; } = new();
    public Line LineBetweenFirstAndEndPoint { get; set; } = new();
    public Polygon PolygonBetweenFirstAndEndPoint { get; set; } = new();
    public List<Core.Models.Polygons.Polygon> Polygons { get; set; } = new();
    public List<System.Windows.Shapes.Polygon> Arrows { get; set; } = new();
    public List<string> PolygonNames { get; set; } = new();

    public Brush Brush { get; set; } = RandomColor.Get().GetBrushColor();

    public StackPanel StackPanel { get; init; }
    public Canvas Canvas { get; init; }

    public PolygonDrawContext(Canvas canvas, StackPanel stackPanel)
    {
        Canvas = canvas;
        StackPanel = stackPanel;    
    }
}
