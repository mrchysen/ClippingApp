using Core.HullCreators.QuickHull;
using Core.Intersection;
using Core.Models.DoubleLinkedLists;
using Core.Models.Points;
using Core.Models.Polygons;
using Core.Utils.Equalizers;
using System.Diagnostics;
using System.Linq;

namespace Core.HullCreators.NoncovexAlgorithms;

// https://na-journal.ru/5-2022-informacionnye-tekhnologii/3730-postroenie-nevypukloi-obolochki-mnozhestva-tochek?ysclid=m7iyumppat211114331
public class PoogachevAlgorithm : INonconvexCreator
{
    private readonly SegmentAndPolygonIntersector _intersector;
    private readonly IEqualizer<PointD> _equalizer;

    private int _parameter;
    private HashSet<PointD> _visited = new();

    public PoogachevAlgorithm(
        int parameter,
        SegmentAndPolygonIntersector? intersector = null,
        IEqualizer<PointD>? equalizer = null)
    {
        _parameter = parameter;
        _intersector = intersector ?? new();
        _equalizer = equalizer ?? new PointDEqualizer();
    }

    public Polygon CreateHull(List<PointD> points, IConvexHullCreator? convexHullCreator = null)
    {
        convexHullCreator = convexHullCreator ?? new QuickHullAlgorithm();

        var polygon = convexHullCreator.CreateHull(points).ToDoubleLinkedList();
        var insidePoints = points.Where(p => !polygon.Contains(p)).ToList();

        var pointCount = polygon.Count;
        var step = 0;
        while(pointCount * _parameter > step)
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
                polygon.Count++;
                insidePoints.Remove(pointToAdd.Value);
            }
            else // No point found
            {
                Debug.WriteLine($"step {step} max {pointCount * _parameter}");
                Debug.WriteLine($"visited {_visited.Count}");

                _visited.Add(pointP.Value);

                if (_visited.Count > points.Count / 2)
                    break;
            }

            step++;
        }

        return polygon.ToPolygon();
    }

    // Пересекают ли PT и QT выпуклую оболочку
    private bool IsCrossHull(DoubleLinkedList<PointD> polygon, Node<PointD> segmentPQ, PointD pointT)
    {
        var firstPoint = polygon.Head;
        var currentPoint = polygon.Head;

        do
        {
            var points = _intersector.GetIntersectionPoint(
                new(segmentPQ.Value, pointT), // PT and polygon
                polygon)
                .Where(p => !_equalizer.IsEquals(p, segmentPQ.Value))
                .ToList();

            if (points.Count > 0)
                return true;

            points = _intersector.GetIntersectionPoint(
                new(segmentPQ.Next.Value, pointT), // PQ and polygon
                polygon)
                .Where(p => !_equalizer.IsEquals(p, segmentPQ.Next.Value))
                .ToList();

            if (points.Count > 0)
                return true;

            currentPoint = currentPoint.Next;
        }
        while (firstPoint != polygon.Head);

        return false;
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

    // Начальная точка отрезочка на полигоне с максимальной длиной
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

            if (!_visited.Contains(firstPoint.Value) && (secondPoint.Value - firstPoint.Value).Norm() > maxDistance)
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
