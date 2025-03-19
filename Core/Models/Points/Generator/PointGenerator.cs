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
        var x = (_area.X + _area.Width - _area.X) * _random.NextDouble() + _area.X;
        var y = (_area.Y + _area.Height - _area.Y) * _random.NextDouble() + _area.Y;

        return new PointD(x, y);
    }
}
