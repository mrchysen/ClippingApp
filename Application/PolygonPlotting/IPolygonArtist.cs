using Application.PolygonPlotting.Models;

namespace Application.PolygonPlotting;

public interface IPolygonArtist
{
	FilePlotInfo Plot(FilePlotInfo info);
}