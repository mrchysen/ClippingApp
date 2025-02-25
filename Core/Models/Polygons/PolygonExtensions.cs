using Core.Clippers.WeilerAthertonPolygonClipper;
using Core.Models.DoubleLinkedLists;
using Core.Models.Points;

namespace Core.Models.Polygons;

public static class PolygonExtensions
{
    public static DoubleLinkedList<PointWithFlag> ToDoubleLinkedListWithFlags(this Polygon polygon)
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

    public static DoubleLinkedList<PointD> ToDoubleLinkedList(this Polygon polygon)
    {
        var list = new DoubleLinkedList<PointD>();

        foreach (var point in polygon.Points)
        {
            list.Add(point);
        }

        return list;
    }
}
