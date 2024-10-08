using Core.Colors;

namespace Core.Models;

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
