using System.Drawing;

namespace Core.Models.Points.Generator;

public interface IPointGenerator
{
    PointD GeneratePoint();
}

public class PointGenerator : IPointGenerator
{
    private readonly Random _random;

    private readonly Rectangle _area;

    public PointGenerator(Rectangle area)
    {
        _random = new Random();
        _area = area;
    }

    public PointD GeneratePoint()
    {
        var x = (_area.Width) * _random.NextDouble() + _area.X;
        var y = (_area.Height) * _random.NextDouble() + _area.Y;

        return new PointD(x, y);
    }
}
