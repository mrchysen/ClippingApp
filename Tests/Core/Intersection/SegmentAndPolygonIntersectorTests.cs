using Core.Intersection;
using Core.Models.Lines;
using Core.Models.Polygons;

namespace Tests.Core.Intersection;

public class SegmentAndPolygonIntersectorTests
{
    private SegmentAndPolygonIntersector _intersector = new();

    [Fact]
    public void GetIntersectionPoint_IntersectionPointIsPolygonPoint_SuchPointInResult()
    {
        // Arrange
        var line = new Line(new(1,1), new(5,5));

        var polygon = new Polygon()
        {
            Points = [
                new(0,0),
                new(0,5),
                new(5,5),
                new(5,0),
                ]
        };
            
        // Act
        var result = _intersector.GetIntersectionPoint(line, polygon);

        // Assert
        Assert.NotNull(result);
        Assert.Contains(new(5,5), result);
    }

    [Fact]
    public void GetIntersectionPoint_PolygonAndLinkedListAsParametr_TheSameResult()
    {
        // Arrange
        var line = new Line(new(1, 1), new(5, 5));

        var polygon = new Polygon()
        {
            Points = [
                new(5,0),
                new(0,0),
                new(0,5),
                new(5,5),
                ]
        };

        var polygonAsLinkedList = polygon.ToDoubleLinkedList();

        // Act
        var result1 = _intersector.GetIntersectionPoint(line, polygon);
        var result2 = _intersector.GetIntersectionPoint(line, polygonAsLinkedList);

        // Assert
        Assert.Equal(result1, result2);
    }
}
