using WindowApp.Infrastructure;

namespace WindowApp.Commands;

public interface ICommand
{
    void Handle(PlotManager plotManager);
}
