using Core.Colors;
using Core.Models.Points;
using Core.Models.Points.Generator;
using Core.Models.Polygons;
using Core.Utils.Extensions;
using System.Drawing;

namespace Application.Randoms;

public class RandomPolygonGenerator : IPolygonGenerator
{
    private readonly Random _random;
    private readonly PointGenerator _pointGenerator;

    public RandomPolygonGenerator(RandomSettings? settings = null)
    {
        settings = settings ?? new RandomSettings();

        _random = new Random();

        var _area = new Rectangle(
            settings.Area.X,
            settings.Area.Y,
            settings.Area.Width,
            settings.Area.Height);

        _pointGenerator = new(_area);
    }

    public Polygon Generate(bool sortByAngle, bool clockwise = false, int count = 3)
    {
        Polygon polygon = new Polygon();

        for (int i = 0; i < count; i++)
        {
            polygon.Points.Add(_pointGenerator.GeneratePoint());
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
            list.Add(_pointGenerator.GeneratePoint());
        }

        return list;
    }
}
