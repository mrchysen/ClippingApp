using Application.Converters.Polygons;
using Application.PolygonPlotting.Models;
using Core.Models.Polygons;
using ScottPlot;

namespace Application.PolygonPlotting;

public class PolygonArtist : IPolygonArtist
{
	protected List<Polygon> Polygons { get; set; } = new();

	public PolygonArtist(List<Polygon> polygons)
	{
		Polygons = polygons;
	}

    public FilePlotInfo Plot(FilePlotInfo info)
	{
		Plot plot = new();

		var polygons = new PolygonConverter().Convert(Polygons);

		polygons.ForEach(el => plot.PlottableList.Add(el));

		var plotInfo = plot.SavePng(
			info.Path, 
			info.PictureSize.Width, 
			info.PictureSize.Width);

		info.FileSize = plotInfo.FileSize;

		return info;
	}
}
