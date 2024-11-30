using ScottPlot;
using ScottPlot.WPF;

namespace WindowApp.Infrastructure;

public class PlotManager
{
    private WpfPlot _wpfPlot;

    public PlotManager(WpfPlot wpfPlot)
    {
        _wpfPlot = wpfPlot;
    }

    public Plot Plot => _wpfPlot.Plot;

    public WpfPlot WpfPlot => _wpfPlot;
}
