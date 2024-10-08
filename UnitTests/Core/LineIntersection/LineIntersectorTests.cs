using Core.Intersection;
using Core.Models;
using FluentAssertions;

namespace UnitTests.Core.LineIntersection;

public class LineIntersectorTests
{
    private readonly LineIntersector _intersector;

    public LineIntersectorTests()
    {
        // ToDo добавить настройку точности и эквалайзера
        _intersector = new LineIntersector();
    }

    [Fact]
    public void GetIntersectionPoint_ParallelLine_NullResult()
    {
        // Arrange
        Line line1 = new(new(0, 0), new(1, 1));

        Line line2 = new(new(0, 0), new(10, 10));

        // Act
        var resultPoint = _intersector.GetIntersectionPoint(line1, line2);

        // Assert
        resultPoint.Should().BeNull();
    }

    [Fact]
    public void GetIntersectionPoint_ParallelLine_NullResult2()
    {
        // Arrange
        Line line1 = new(new(0, 0), new(1, 1));

        Line line2 = new(new(1, 0), new(11, 10));

        // Act
        var resultPoint = _intersector.GetIntersectionPoint(line1, line2);

        // Assert
        resultPoint.Should().BeNull();
    }

    [Fact]
    public void GetIntersectionPoint_NotParallelLine_ReturnPoint1()
    {
        // Arrange
        Line line1 = new(new(0, 0), new(1, 1));

        Line line2 = new(new(1, 0), new(0, 1));

        var expectedPoint = new PointD(0.5, 0.5);

        // Act
        var resultPoint = _intersector.GetIntersectionPoint(line1, line2);

        // Assert
        resultPoint.Should().NotBeNull();
        Assert.True(Math.Abs(resultPoint.Value.X - expectedPoint.X) < _intersector.Epsilon);
        Assert.True(Math.Abs(resultPoint.Value.Y - expectedPoint.Y) < _intersector.Epsilon);
    }

    [Fact]
    public void GetIntersectionPoint_NotParallelLine_ReturnPoint2()
    {
        // Arrange
        Line line1 = new(new(0, -1), new(1, 1));

        Line line2 = new(new(0, 1), new(-1, 4));

        var expectedPoint = new PointD(0.4, -0.2);

        // Act
        var resultPoint = _intersector.GetIntersectionPoint(line1, line2);

        // Assert
        resultPoint.Should().NotBeNull();
        Assert.True(Math.Abs(resultPoint.Value.X - expectedPoint.X) < _intersector.Epsilon);
        Assert.True(Math.Abs(resultPoint.Value.Y - expectedPoint.Y) < _intersector.Epsilon);
    }

    [Fact]
    public void GetIntersectionPoint_NotParallelLine_ReturnPoint3()
    {
        // Arrange
        Line line1 = new(new(0, 0), new(3, -2));

        Line line2 = new(new(2, 0), new(5, 4));

        var expectedPoint = new PointD(4.0 / 3.0, -8.0 / 9.0);

        // Act
        var resultPoint = _intersector.GetIntersectionPoint(line1, line2);

        // Assert
        resultPoint.Should().NotBeNull();
        Assert.True(Math.Abs(resultPoint.Value.X - expectedPoint.X) < _intersector.Epsilon);
        Assert.True(Math.Abs(resultPoint.Value.Y - expectedPoint.Y) < _intersector.Epsilon);
    }
}
