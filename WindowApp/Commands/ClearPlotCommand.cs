using WindowApp.Infrastructure;

namespace WindowApp.Commands;

public class ClearPlotCommand : IMainWindowCommand
{
    private PlotManager _plotManager;

    public ClearPlotCommand(PlotManager plotManager)
    {
        _plotManager = plotManager;
    }

    public Task Handle()
    {
        _plotManager.Clear();

        return Task.CompletedTask;
    }
}
