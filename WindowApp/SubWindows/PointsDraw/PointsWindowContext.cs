using Core.Models.Points;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace WindowApp.SubWindows.PointsDraw;

public class PointsWindowContext
{
    public PointsWindowContext(Canvas canvas)
    {
        Canvas = canvas;
    }

    public List<PointD> Points { get; set; } = new();

    public List<Ellipse> Ellipses { get; set; } = new();

    public Canvas Canvas { get; init; }
}
