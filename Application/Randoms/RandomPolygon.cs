using Core.Colors;
using Core.Models;
using Core.Models.Polygons;
using Core.Utils.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Drawing;

namespace Application.Randoms;

public class RandomPolygon
{
    private readonly Random _random;
    private readonly Rectangle _area;

    public RandomPolygon(Random random, IOptions<RandomSettings> options)
    {
        var settings = options.Value;

        _random = random;

        _area = new Rectangle(
            settings.Area.X,
            settings.Area.Y,
            settings.Area.Width,
            settings.Area.Height);
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
        var x = (_area.X + _area.Width - _area.X) * _random.NextDouble() + _area.X;
        var y = (_area.Y + _area.Height - _area.Y) * _random.NextDouble() + _area.Y;

        return new PointD(x, y);
    }
}
