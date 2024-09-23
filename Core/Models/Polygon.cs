using System.Drawing;

namespace Core.Models;

public class Polygon
{
    public List<Point> Points { get; set; } = new List<Point>();

    public CoreColor Color { get; set; } = new();
}
