using Core.Clippers.WeilerAthertonPolygonClipper;
using Core.Models.Points;
using Core.Models.Polygons;

namespace Core.Models.DoubleLinkedLists;

public static class DoubleLinkedListPolygonExtensions
{
    public static Polygon ToPolygon(this DoubleLinkedList<PointD> list) 
        => new([.. list]);

    public static bool AddBeetwenInOrder(this DoubleLinkedList<PointWithFlag> list, 
        Node<PointWithFlag> from, 
        Node<PointWithFlag> to,
        Node<PointWithFlag> pointToAdd)
    {
        if(from.Next == to)
        {
            InsertNodeBetween(from, to, pointToAdd);

            list.Count++;

            return true;
        }

        var next = from.Next;
        bool inserted = false;

        
        while (next.Prev != to)
        {
            if (IsPointOnSegment(pointToAdd.Value.Point, next.Prev.Value.Point, next.Value.Point))
            {
                InsertNodeBetween(next.Prev, next, pointToAdd);
                inserted = true;
                list.Count++;

                break;
            }

            next = next.Next;
        }

        return inserted;
    }

    private static void InsertNodeBetween<T>(Node<T> from, Node<T> to, Node<T> item)
    {
        item.Next = to;
        item.Prev = from;

        from.Next = item;
        to.Prev = item;
    }

    // ToDo: вынести куда-нибудь
    private static bool IsPointOnSegment(PointD p, PointD seg1, PointD seg2, double Eps = 0.001)
    {
        var vec1 = p - seg1;
        var vec2 = seg1 - seg2;

        bool onLine = (vec1 * vec2) < Eps;

        bool withinX = Math.Min(seg1.X, seg2.X) - Eps <= p.X && p.X <= Math.Max(seg1.X, seg2.X) + Eps;
        bool withinY = Math.Min(seg1.Y, seg2.Y) - Eps <= p.Y && p.Y <= Math.Max(seg1.Y, seg2.Y) + Eps;

        return onLine && withinX && withinY;
    }
}
