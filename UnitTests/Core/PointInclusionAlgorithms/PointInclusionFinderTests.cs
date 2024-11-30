using Core.Models;
using Core.Models.Polygons;
using Core.PointInclusionAlgorithms;
using System.Collections.Generic;
using System.Drawing;

namespace UnitTests.Core.PointInclusionAlgorithms;

public class PointInclusionFinderTests
{
	private PointPolygonInclusionFinder Finder;

	private PointInclusionFinderTests()
	{
		Finder = new();
	}

	[Fact]
	public void CheckPointInsidePolygon_PointInside_Success()
	{
		// Arrange
		Polygon polygon = new Polygon()
		{
			Points = new List<PointD>()
			{
				new PointD(1,1),
				new PointD(40, 40),
				new PointD(100, 100)
			}
		};

		// Act

		// Assert
	}
}
