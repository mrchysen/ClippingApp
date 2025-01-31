using Core.Models.DoubleLinkedLists;
using Core.Models.Points;

namespace Core.Clippers.WeilerAthertonPolygonClipper;

public class PointWithFlag
{
    public PointD Point { get; set; }

    public PointFlag Flag { get; set; } = PointFlag.Internal;

    public bool HasLink => Flag == PointFlag.Exit || Flag == PointFlag.Entry;

    public Node<PointWithFlag>? LinkNode { get; set; }
}
