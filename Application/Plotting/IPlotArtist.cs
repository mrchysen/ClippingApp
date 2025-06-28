using ScottPlot;

namespace Application.PolygonPlotting;

public interface IPlotArtist
{
    Plot Draw(Plot? plotInput);
}
