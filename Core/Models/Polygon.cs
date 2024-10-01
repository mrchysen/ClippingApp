using System.Drawing;

namespace Core.Models;

public class Polygon
{
    public List<PointD> Points { get; set; } = new List<PointD>();

    public CoreColor Color { get; set; } = new();
}
