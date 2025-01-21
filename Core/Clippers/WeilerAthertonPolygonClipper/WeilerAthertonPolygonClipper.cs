using Core.Models.Points;
using Core.Models.Polygons;

namespace Core.Clippers.WeilerAthertonPolygonClipper;

enum Operation
{
    Union,
    Intersect,
    Difference
}

struct Pair<T> : IEquatable<Pair<T>>
{
    readonly T first;
    readonly T second;

    public Pair(T first, T second)
    {
        this.first = first;
        this.second = second;
    }

    public T First { get { return first; } }
    public T Second { get { return second; } }

    public override int GetHashCode()
    {
        return first.GetHashCode() ^ second.GetHashCode();
    }

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }
        return Equals((Pair<T>)obj);
    }

    public bool Equals(Pair<T> other)
    {
        return other.first.Equals(first) && other.second.Equals(second) ||
                other.first.Equals(second) && other.second.Equals(first);
    }
}

public class WeilerAthertonPolygonClipper : IClipper
{
    private List<CircularLinkedList<PointD>> Process(
        CircularLinkedList<PointD> subject,
        CircularLinkedList<PointD> clip,
        Operation operation)
    {
        LinkedListNode<PointD> curSubject = subject.First;
        Dictionary<PointD, Pair<PointD>> intersections = new Dictionary<PointD, Pair<PointD>>();
        List<CircularLinkedList<PointD>> polygons = new List<CircularLinkedList<PointD>>();

        if (AreEqual(subject, clip))
        {
            switch (operation)
            {
                case Operation.Union:
                    polygons.Add(subject);
                    return polygons;
                case Operation.Intersect:
                    polygons.Add(subject);
                    return polygons;
                case Operation.Difference:
                    // нужно как-то разграничивать внешние и внутренние полигоны
                    return polygons;
                default:
                    break;
            }
        }

        while (curSubject != subject.Last)
        {
            LinkedListNode<PointD> curClip = clip.First;
            while (curClip != clip.Last)
            {
                PointD intersectionPoint; // можно заменить на свой SegmentIntersector
                if (IntersectSegment(curSubject.Value,
                                    curSubject.Next.Value,
                                    curClip.Value,
                                    curClip.Next.Value,
                                    out intersectionPoint))
                {
                    subject.AddAfter(curSubject, intersectionPoint);
                    clip.AddAfter(curClip, intersectionPoint);
                    intersections.Add(intersectionPoint, new Pair<PointD>(curClip.Value, curClip.Next.Value));
                }
                curClip = curClip.Next;
            }
            curSubject = curSubject.Next;
        }

        CircularLinkedList<PointD> entering = new CircularLinkedList<PointD>();
        CircularLinkedList<PointD> exiting = new CircularLinkedList<PointD>();

        MakeEnterExitList(subject, clip, intersections, entering, exiting);
        subject.RemoveLast();
        clip.RemoveLast();

        Traverse(subject, clip, entering, exiting, polygons, operation);

        if (polygons.Count == 0)
        {
            switch (operation)
            {
                case Operation.Union:
                    polygons.Add(subject);
                    polygons.Add(clip);
                    break;
                case Operation.Intersect:
                    break;
                case Operation.Difference:
                    polygons.Add(subject);
                    break;
                default:
                    break;
            }
        }

        return polygons;
    }

    private bool AreEqual<T>(LinkedList<T> a, LinkedList<T> b)
    {
        if (a.Count != b.Count)
        {
            return false;
        }

        LinkedListNode<T> currA = a.First;
        LinkedListNode<T> currB = b.First;

        while (currA != null)
        {
            if (!currA.Value.Equals(currB.Value))
                return false;

            currA = currA.Next;
            currB = currB.Next;
        }

        return true;
    }

    private void Swap<T>(ref T left, ref T right) where T : class
    {
        T temp;
        temp = left;
        left = right;
        right = temp;
    }

    private void Traverse(
        CircularLinkedList<PointD> subject,
        CircularLinkedList<PointD> clip,
        CircularLinkedList<PointD> entering,
        CircularLinkedList<PointD> exiting,
        List<CircularLinkedList<PointD>> polygons,
        Operation operation)
    {
        if (operation == Operation.Intersect)
            Swap(ref entering, ref exiting);

        if (operation == Operation.Difference)
            clip = clip.Reverse();

        CircularLinkedList<PointD> currentList = subject;
        CircularLinkedList<PointD> otherList = clip;

        while (entering.Count > 0)
        {
            CircularLinkedList<PointD> polygon = new CircularLinkedList<PointD>();
            PointD start = entering.First.Value;
            int count = 0;
            LinkedListNode<PointD> transitionNode = entering.First;
            bool enteringCheck = true;

            while (transitionNode != null && (count == 0 || count > 0 && start != transitionNode.Value))
            {
                transitionNode = TraverseList(currentList, entering, exiting, polygon, transitionNode, start, otherList);

                enteringCheck = !enteringCheck;

                if (currentList == subject)
                {
                    currentList = clip;
                    otherList = subject;
                }
                else
                {
                    currentList = subject;
                    otherList = clip;
                }

                count++;
            }

            polygons.Add(polygon);
        }
    }

    private static LinkedListNode<PointD> TraverseList(
        CircularLinkedList<PointD> contour,
        CircularLinkedList<PointD> entering,
        CircularLinkedList<PointD> exiting,
        CircularLinkedList<PointD> polygon,
        LinkedListNode<PointD> currentNode,
        PointD startNode,
        CircularLinkedList<PointD> contour2)
    {
        LinkedListNode<PointD> contourNode = contour.Find(currentNode.Value);
        if (contourNode == null)
            return null;

        entering.Remove(currentNode.Value);

        while (contourNode != null
                &&
                    !entering.Contains(contourNode.Value)
                    &&
                    !exiting.Contains(contourNode.Value)
                )
        {
            polygon.AddLast(contourNode.Value);
            contourNode = contour.NextOrFirst(contourNode);

            if (contourNode.Value == startNode)
                return null;
        }

        entering.Remove(contourNode.Value);
        polygon.AddLast(contourNode.Value);

        return contour2.NextOrFirst(contour2.Find(contourNode.Value));
    }

    private void MakeEnterExitList(
        CircularLinkedList<PointD> subject,
        CircularLinkedList<PointD> clip,
        Dictionary<PointD, Pair<PointD>> intersections,
        CircularLinkedList<PointD> entering,
        CircularLinkedList<PointD> exiting)
    {
        LinkedListNode<PointD> curr = subject.First;
        while (curr != subject.Last)
        {
            if (intersections.ContainsKey(curr.Value))
            {
                bool isEntering = PointDCross(curr.Next.Value.X - curr.Previous.Value.X,
                                              curr.Next.Value.Y - curr.Previous.Value.Y,
                                              intersections[curr.Value].Second.X - intersections[curr.Value].First.X,
                                              intersections[curr.Value].Second.Y - intersections[curr.Value].First.Y) < 0;

                if (isEntering)
                    entering.AddLast(curr.Value);
                else
                    exiting.AddLast(curr.Value);
            }

            curr = curr.Next;
        }
    }

    private double PointDCross(double x1, double y1, double x2, double y2)
    {
        return x1 * y2 - x2 * y1;
    }

    // можно заменить на свой SegmentIntersector
    private bool IntersectSegment(PointD start1, PointD end1, PointD start2, PointD end2, out PointD intersection)
    {
        //addIntersection = true;
        PointD dir1 = new PointD(end1.X - start1.X, end1.Y - start1.Y);
        PointD dir2 = new PointD(end2.X - start2.X, end2.Y - start2.Y);

        //считаем уравнения прямых проходящих через отрезки
        double a1 = -dir1.Y;
        double b1 = +dir1.X;
        double d1 = -(a1 * start1.X + b1 * start1.Y);

        double a2 = -dir2.Y;
        double b2 = +dir2.X;
        double d2 = -(a2 * start2.X + b2 * start2.Y);

        //подставляем концы отрезков, для выяснения в каких полуплоскотях они
        double seg1_line2_start = a2 * start1.X + b2 * start1.Y + d2;
        double seg1_line2_end = a2 * end1.X + b2 * end1.Y + d2;

        double seg2_line1_start = a1 * start2.X + b1 * start2.Y + d1;
        double seg2_line1_end = a1 * end2.X + b1 * end2.Y + d1;

        //если концы одного отрезка имеют один знак, значит он в одной полуплоскости и пересечения нет.
        if (seg1_line2_start * seg1_line2_end >= 0 || seg2_line1_start * seg2_line1_end >= 0)
        {
            intersection = new PointD(0, 0);
            return false;
        }

        double u = seg1_line2_start / (seg1_line2_start - seg1_line2_end);
        intersection = new PointD(start1.X + dir1.X * u, start1.Y + dir1.Y * u);

        return true;
    }

    public List<Polygon> Clip(List<Polygon> polygons)
    {
        throw new NotImplementedException();
    }

    public List<Polygon> Clip(Polygon polygon1, Polygon polygon2)
    {
        CircularLinkedList<PointD> pol1 = new();
        polygon1.Points.ForEach(p => pol1.AddLast(p));

        CircularLinkedList<PointD> pol2 = new();
        polygon2.Points.ForEach(p => pol2.AddLast(p));

        var result = Process(pol1, pol2, Operation.Intersect);

        var polygons = result.Select(el =>
        {
            var points = el.ToList();

            return new Polygon(points);
        }).ToList();

        return polygons;
    }
}