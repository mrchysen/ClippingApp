using Core.LineIntersection;
using Core.Models;
using FluentAssertions;

namespace UnitTests.LineIntersection;

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
		Line line1 = new(new(0,0), new(1,1));

		Line line2 = new(new(0,0), new(10,10));

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
	public void GetIntersectionPoint_NotParallelLine_NullResult2()
	{
		// Arrange
		Line line1 = new(new(0, 0), new(1, 1));

		Line line2 = new(new(1, 0), new(0, 1));

		// Act
		var resultPoint = _intersector.GetIntersectionPoint(line1, line2);

		// Assert
		resultPoint.Should().NotBeNull();
		Assert.True(Math.Abs(resultPoint.Value.X - 0.5) < _intersector.Epsilon);
		Assert.True(Math.Abs(resultPoint.Value.Y - 0.5) < _intersector.Epsilon);
	}
}
