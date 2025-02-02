using Core.Colors;
using Core.Models.Points;
using Core.Models.Polygons;
using Core.Utils.Extensions;
using Microsoft.Extensions.Options;
using System.Drawing;

namespace Application.Randoms;

public class RandomPolygonGenerator : IPolygonGenerator
{
    private readonly Random _random;
    private readonly Rectangle _area;

    public RandomPolygonGenerator(Random random, IOptions<RandomSettings> options)
    {
        var settings = options.Value;

        _random = random;

        _area = new Rectangle(
            settings.Area.X,
            settings.Area.Y,
            settings.Area.Width,
            settings.Area.Height);
    }

    public Polygon Generate(bool sortByAngle, bool clockwise = false, int count = 3)
    {
        Polygon polygon = new Polygon();

        for (int i = 0; i < count; i++)
        {
            polygon.Points.Add(GetPointInArea());
        }

        polygon.Color = RandomColor.Get();

        if (sortByAngle)
        {
            polygon.Points = (clockwise) ? 
                polygon.Points.OrderClockwise().ToList() :
                polygon.Points.OrderClockwise().Reverse().ToList();
        }

        return polygon;
    }

    public List<PointD> GeneratePoints(int count = 10)
    {
        List<PointD> list = new();

        for (int i = 0; i < count; i++)
        {
            list.Add(GetPointInArea());
        }

        return list;
    }

    private PointD GetPointInArea()
    {
        var x = (_area.X + _area.Width - _area.X) * _random.NextDouble() + _area.X;
        var y = (_area.Y + _area.Height - _area.Y) * _random.NextDouble() + _area.Y;

        return new PointD(x, y);
    }
}
