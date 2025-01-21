using Application.PolygonPlotting.Models;
using ScottPlot;

namespace Application.PolygonPlotting;

public interface IPolygonArtist
{
	FilePlotInfo Plot(FilePlotInfo info);

	Plot Plot(Plot? plotInput, bool addMarkers);
}