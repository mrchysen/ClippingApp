using Core.Colors;
using Core.Models.Colors;

namespace Core.Models.Polygons;

public class Polygon
{
    public Polygon() { }

    public Polygon(List<PointD> points)
    {
        Points = points;
        Color = RandomColor.Get();
    }

    public List<PointD> Points { get; set; } = new List<PointD>();

    public CoreColor Color { get; set; } = new();


}
