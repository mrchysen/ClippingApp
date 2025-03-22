using WindowApp.Infrastructure;

namespace WindowApp.Commands;

public interface ICommand
{
    Task Handle(PlotManager plotManager);
}
