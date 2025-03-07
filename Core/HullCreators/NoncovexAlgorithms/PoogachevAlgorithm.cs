using Core.HullCreators.QuickHull;
using Core.Intersection;
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
        var insidePoints = points.Where(p => !polygon.Contains(p)).ToList();

        var pointCount = polygon.Count;
        var step = 0;
        while(pointCount * _count > step)
        {
            var pointP = GetStartNodeWithMaxDistance(polygon);

            var distancePQ = (pointP.Value - pointP.Next.Value).Norm(2);

            var maxArea = 0.0d;
            PointD? pointToAdd = null;

            foreach(var pointT in insidePoints)
            {
                var distancePT = (pointP.Value - pointT).Norm(2);
                var distanceQT = (pointP.Next.Value - pointT).Norm(2);
            
                if(distancePQ > Math.Abs(distancePT - distanceQT))
                {
                    var currentArea = GetArea(pointT, pointP.Value, pointP.Next.Value);

                    if (currentArea < maxArea) continue;
                    if (NotEmptyTriangle(pointP.Value, pointP.Next.Value, pointT, insidePoints)) continue;
                    if (IsCrossHull(polygon, pointP, pointT)) continue;

                    pointToAdd = pointT;
                    maxArea = currentArea;
                }
            }

            if(pointToAdd != null)
            {
                pointP.AddAfter(pointToAdd.Value);
                insidePoints.Remove(pointToAdd.Value);
            }
            else // No points
            {
                break;
            }

            step++;
        }

        return polygon.ToPolygon();
    }

    // Пересекают ли PT и QT выпуклую оболочку
    private bool IsCrossHull(DoubleLinkedList<PointD> polygon, Node<PointD> value, PointD pointT)
    {
        var firstPoint = polygon.Head;
        var secondPoint = firstPoint.Next;
        var intersector = new SegmentIntersector();

        do
        {
            firstPoint = secondPoint;
            secondPoint = secondPoint.Next;

            if ((secondPoint.Value - firstPoint.Value).Norm() > maxDistance)
            {

            }
        }
        while (firstPoint != polygon.Head);
    }

    private bool NotEmptyTriangle(PointD p1, PointD p2, PointD p3, List<PointD> points)
    {
        foreach (var point in points)
        {
            if(point != p3 && HullHelperUtils.IsPointInTriangle(p1,p2,p3,point))
                return true;
        }
        
        return false;
    }

    // Начальная точка отрезока на полигоне с максимальной длиной
    private Node<PointD> GetStartNodeWithMaxDistance(DoubleLinkedList<PointD> polygon)
    {
        var firstPoint = polygon.Head;
        var secondPoint = firstPoint.Next;
        var maxDistance = (secondPoint.Value - firstPoint.Value).Norm();
        var answerPoint = firstPoint;

        do
        {
            firstPoint = secondPoint;
            secondPoint = secondPoint.Next;

            if ((secondPoint.Value - firstPoint.Value).Norm() > maxDistance)
            {
                answerPoint = firstPoint;
                maxDistance = (secondPoint.Value - firstPoint.Value).Norm();
            }
        }
        while (firstPoint != polygon.Head);

        return answerPoint;
    }

    private double GetArea(PointD p1, PointD p2, PointD p3) 
        => Math.Abs((p1 - p2) * (p2 - p3)) / 2;
}
