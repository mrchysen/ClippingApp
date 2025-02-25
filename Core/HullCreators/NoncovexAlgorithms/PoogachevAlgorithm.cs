using Core.HullCreators.QuickHull;
using Core.Models.DoubleLinkedLists;
using Core.Models.Points;
using Core.Models.Polygons;

namespace Core.HullCreators.NoncovexAlgorithms;

// https://na-journal.ru/5-2022-informacionnye-tekhnologii/3730-postroenie-nevypukloi-obolochki-mnozhestva-tochek?ysclid=m7iyumppat211114331
public class PoogachevAlgorithm : INonconvexCreator
{
    private int _count;

    public PoogachevAlgorithm(int count)
    {
        _count = count;
    }

    public Polygon CreateHull(List<PointD> points, IConvexHullCreator? convexHullCreator = null)
    {
        convexHullCreator = convexHullCreator ?? new QuickHullAlgorithm();

        var polygon = convexHullCreator.CreateHull(points).ToDoubleLinkedList();

        var pointCount = polygon.Count;
        var step = 0;
        while(pointCount * _count < step)
        {
            var maxNode = GetNodeWithMaxDistance(polygon);

            step++;
        }

        return polygon.ToPolygon();
    }

    public Node<PointD> GetNodeWithMaxDistance(DoubleLinkedList<PointD> polygon)
    {
        var firstPoint = polygon.Head;
    }
}
