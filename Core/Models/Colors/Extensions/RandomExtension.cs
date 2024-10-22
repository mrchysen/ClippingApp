using Core.Colors;
using Core.Models.Polygons;

namespace Core.Models.Colors.Extensions;

public static class RandomExtension
{
    public static IEnumerable<Polygon> RandomColors(this IEnumerable<Polygon> polygons)
    {
        foreach (var polygon in polygons)
        {
            polygon.Color = RandomColor.Get();
        }

        return polygons;
    }
}
