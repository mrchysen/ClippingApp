using Microsoft.Extensions.Options;
using System.Diagnostics;
using WindowApp.Infrastructure;
using WindowApp.Settings;

namespace WindowApp.Commands;

public class OpenFolderCommand : ICommand
{
    private readonly FilesPathSettings _settings;

    public OpenFolderCommand(IOptions<FilesPathSettings> options)
    {
        _settings = options.Value;
    }

    public Task Handle(PlotManager plotManager)
    {
        Process.Start(new ProcessStartInfo { FileName = _settings.GetPolygonDataFolderPath, UseShellExecute = true });

        return Task.CompletedTask;
    }
}
