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
    public Line LineBetwenFirstAndEndPoint { get; set; } = new();
    public List<Core.Models.Polygons.Polygon> Polygons { get; set; } = new();
    public List<string> PolygonNames { get; set; } = new();

    public Brush Brush { get; set; } = RandomColor.Get().GetBrushColor();
    public Canvas Canvas { get; init; }
    public StackPanel StackPanel { get; init; }

    public PolygonDrawContext(Canvas canvas, StackPanel stackPanel)
    {
        Canvas = canvas;
        StackPanel = stackPanel;    
    }
}
