using Core.Clippers.WeilerAthertonPolygonClipper;
using Core.Models.DoubleLinkedLists;

namespace Core.Models.Polygons;

public static class PolygonExtensions
{
    public static DoubleLinkedList<PointWithFlag> ToDoubleLinkedList(this Polygon polygon)
    {
        var list = new DoubleLinkedList<PointWithFlag>();

        foreach (var point in polygon.Points)
        {
            list.Add(new PointWithFlag()
            {
                Point = point,
                Flag = PointFlag.Internal
            });
        }

        return list;
    }
}
