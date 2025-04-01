using DAL.Files.Polygons;
using Microsoft.Win32;
using WindowApp.Infrastructure;
using WindowApp.Settings;
using Core.Models.Colors.Extensions;

namespace WindowApp.Commands;

public class OpenFileCommand : IMainWindowCommand
{
    private FilesPathSettings _filesPathSettings;
    private PlotManager _plotManager;

    public OpenFileCommand(PlotManager plotManager, FilesPathSettings? filesPathSettings = null)
    {
        _filesPathSettings = filesPathSettings ?? new();
        _plotManager = plotManager;
    }

    public Task Handle()
    {
        var pathToFile = AskForFile();

        if (string.IsNullOrWhiteSpace(pathToFile))
            return Task.CompletedTask;

        var polygons = new PolygonFileLoader(pathToFile)
            .Load()
            .RandomColors()
            .ToList();

        _plotManager.DrawCurrentPolygons(polygons);

        return Task.CompletedTask;
    }

    private string AskForFile()
    {
        OpenFileDialog openFileDialog = new OpenFileDialog();

        openFileDialog.InitialDirectory = _filesPathSettings.GetPolygonDataFolderPath;

        openFileDialog.DefaultExt = ".json";

        var result = openFileDialog.ShowDialog() ?? false;

        if (!result)
        {
            return String.Empty;
        }

        return openFileDialog.FileName;
    }
}