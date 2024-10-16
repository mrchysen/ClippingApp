﻿using Core.Models.Polygons;
using ScottPlot;

namespace Application.Converters.Polygons;

public class PolygonConverter
{
	public ScottPlot.Plottables.Polygon Convert(Polygon sourcePolygon)
	{
		var point = sourcePolygon.Points
			.Select(el => new Coordinates(el.X, el.Y))
			.ToArray();

		var destinationPolygon = new ScottPlot.Plottables.Polygon(point);
		
		destinationPolygon.FillColor = new ScottPlot.Color(
			sourcePolygon.Color.R, 
			sourcePolygon.Color.G, 
			sourcePolygon.Color.B);

		return destinationPolygon;
	}

	public List<ScottPlot.Plottables.Polygon> Convert(List<Polygon> sourcePolygons)
		=> sourcePolygons.Select(Convert).ToList();
}