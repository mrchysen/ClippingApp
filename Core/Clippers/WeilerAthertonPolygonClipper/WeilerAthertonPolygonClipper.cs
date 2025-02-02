using Core.Intersection;
using Core.Models.Colors;
using Core.Models.DoubleLinkedLists;
using Core.Models.Points;
using Core.Models.Polygons;
using System.Diagnostics;

namespace Core.Clippers.WeilerAthertonPolygonClipper;

public class WeilerAthertonPolygonClipper : IClipper
{
    private DoubleLinkedList<PointWithFlag> _polygon1 = null!;
    private DoubleLinkedList<PointWithFlag> _polygon2 = null!;
    private HashSet<PointD> visited = new();

    private readonly SegmentIntersector _segmentIntersector;

    public WeilerAthertonPolygonClipper(SegmentIntersector segmentIntersector)
    {
        _segmentIntersector = segmentIntersector;
    }

    public List<Polygon> Clip(List<Polygon> polygons)
    {
        throw new NotImplementedException();
    }

    // clockwise
    public List<Polygon> Clip(Polygon polygon1, Polygon polygon2)
    {
        _polygon1 = polygon1.ToDoubleLinkedList();
        _polygon2 = polygon2.ToDoubleLinkedList();

        FindAllIntersectionAndLinkedThem();

        var polygons = CutPolygons();

        return polygons.Select(x =>
        {
            var polygon = x.ToPolygon();
            polygon.Color = CoreColor.IntersectColors(polygon1.Color, polygon2.Color);
            return polygon;
        }).ToList();
    }

    private List<DoubleLinkedList<PointD>> CutPolygons()
    {
        List<DoubleLinkedList<PointD>> polygons = new();

        var currentNode = _polygon1.Head;

        while (true)
        {
            if (!visited.Contains(currentNode.Value.Point) &&
                currentNode.Value.Flag == PointFlag.Entry)
            {
                polygons.Add(WalkLoop(currentNode));
            }

            if (currentNode.Next == _polygon1.Head)
            {
                break;
            }
            else
            {
                currentNode = currentNode.Next;
            }
        }

        return polygons;
    }

    private DoubleLinkedList<PointD> WalkLoop(Node<PointWithFlag> currentNode)
    {
        DoubleLinkedList<PointD> list = new();

        visited.Add(currentNode.Value.Point);
        list.Add(currentNode.Value.Point);
        var next = currentNode.Next;

        while (next.Value.Point != currentNode.Value.Point)
        {
            list.Add(next.Value.Point);

            // тут могут быть неприятные циклы
            // https://liorsinai.github.io/mathematics/2023/09/30/polygon-clipping.html#walk-linked-lists

            visited.Add(next.Value.Point);

            if (next.Value.HasLink)
            {
                next = next.Value.LinkNode!;
            }

            next = next.Next;
        }

        return list;
    }

    private void FindAllIntersectionAndLinkedThem()
    {
        List<(PointWithFlag intP, Node<PointWithFlag> fromP, Node<PointWithFlag> intoP,
            Node<PointWithFlag> fromQ, Node<PointWithFlag> intoQ)> polygonsIntersections = new();

        var p = _polygon1.Head;
        var pNext = _polygon1.Head.Next;
        var countP = 0;

        while (countP < _polygon1.Count)
        {
            var q = _polygon2.Head;
            var qNext = _polygon2.Head.Next;
            var countQ = 0;

            while (countQ < _polygon2.Count)
            {
                var intPoint = _segmentIntersector.GetIntersectionPoint(
                    new(p.Value.Point, pNext.Value.Point),
                    new(q.Value.Point, qNext.Value.Point));

                if (intPoint != null)
                {
                    var pVec = p.Value.Point - pNext.Value.Point;
                    var qVec = q.Value.Point - qNext.Value.Point;

                    var flag = qVec * pVec < 0 ? PointFlag.Entry : PointFlag.Exit;

                    polygonsIntersections.Add((
                        new PointWithFlag()
                        {
                            Flag = flag,
                            Point = intPoint.Value
                        },
                        p,
                        pNext,
                        q,
                        qNext));
                }

                countQ++;

                q = q.Next;
                qNext = q.Next;
            }

            countP++;

            p = p.Next;
            pNext = p.Next;
        }

        foreach (var item in polygonsIntersections)
        {
            var nodeP = new Node<PointWithFlag>();
            nodeP.Value = new PointWithFlag()
            {
                Point = item.intP.Point,
                Flag = item.intP.Flag,
            };

            var nodeQ = new Node<PointWithFlag>();
            nodeQ.Value = new PointWithFlag()
            {
                Point = item.intP.Point,
                Flag = item.intP.Flag,
                LinkNode = nodeP
            };

            nodeP.Value.LinkNode = nodeQ;

            _polygon1.AddBeetwenInOrder(item.fromP, item.intoP, nodeP);
            _polygon2.AddBeetwenInOrder(item.fromQ, item.intoQ, nodeQ);
        }
    }

    private void DebugPrintInfo(DoubleLinkedList<PointWithFlag> list)
    {
        Debug.WriteLine($"--- list start ---");
        var c = 0;
        foreach (var item in list)
        {
            if (c == 3)
            {
                c = 0;
                Debug.Write("\n");
            }
            Debug.Write($"{item.Point} {item.Flag.ToString()} ->> ");
            c++;
        }
        Debug.Write("\n");
        Debug.WriteLine($"--- list end ---");
    }

    private void DebugPrintInfo(DoubleLinkedList<PointD> list)
    {
        Debug.WriteLine($"--- list start ---");
        var c = 0;
        foreach (var item in list)
        {
            if (c == 3)
            {
                c = 0;
                Debug.Write("\n");
            }
            Debug.Write($"{item} ->> ");
            c++;
        }
        Debug.Write("\n");
        Debug.WriteLine($"--- list end ---");
    }
}
