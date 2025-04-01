using System.Diagnostics;
using WindowApp.Settings;

namespace WindowApp.Commands;

public class OpenFolderCommand : IMainWindowCommand
{
    private readonly FilesPathSettings _settings;

    public OpenFolderCommand(FilesPathSettings? settings = null)
    {
        _settings = settings ?? new();
    }

    public Task Handle()
    {
        Process.Start(new ProcessStartInfo { FileName = _settings.GetPolygonDataFolderPath, UseShellExecute = true });

        return Task.CompletedTask;
    }
}
