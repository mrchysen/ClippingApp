using Core.Colors;
using Core.Utils.Extensions;
using System.Drawing;

namespace Core.Models.Polygons;

public class RandomPolygon
{
    public Random Random { get; set; }

	public Rectangle Area { get; set; } = new(0, 0, 100, 100);

    public RandomPolygon()
	{
		Random = new Random();
	}
    public RandomPolygon(int seed) 
	{ 
		Random = new Random(seed);
	}

	public Polygon Get(bool sortByAngle, int vertex = 3)
	{
		Polygon polygon = new Polygon();

		for (int i = 0; i < vertex; i++) 
		{
			polygon.Points.Add(GetPointInArea());
		}

		polygon.Color = RandomColor.Get();

		if (sortByAngle)
		{
			polygon.Points = polygon.Points.OrderClockwise().ToList();
		}

		return polygon;
	}

	private PointD GetPointInArea()
	{
		var x = ((Area.X + Area.Width) - Area.X) * Random.NextDouble() + Area.X;
		var y = ((Area.Y + Area.Height) - Area.Y) * Random.NextDouble() + Area.Y;

		return new PointD(x, y);
	}
}
