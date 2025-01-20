using Core.Colors;
using Core.Models.Colors;
using Core.Models.Points;
using System.Text.Json.Serialization;

namespace Core.Models.Polygons;

public class Polygon
{
    public Polygon() { }

    public Polygon(List<PointD> points)
    {
        Points = points;
        Color = RandomColor.Get();
    }

    public int Count => Points.Count;

    public List<PointD> Points { get; set; } = new List<PointD>();

    [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
    public CoreColor Color { get; set; } = new();


}
