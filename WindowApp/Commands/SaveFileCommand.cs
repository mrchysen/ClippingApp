using DAL.Files.Polygons;
using System.IO;
using System.Windows;
using WindowApp.Infrastructure;
using WindowApp.Settings;

namespace WindowApp.Commands;

public class SaveFileCommand : IMainWindowCommand
{
    private FilesPathSettings _filesPathSettings;
    private PlotManager _plotManager;

    public SaveFileCommand(PlotManager plotManager, FilesPathSettings? filesPathSettings = null)
    {
        _filesPathSettings = filesPathSettings ?? new();
        _plotManager = plotManager;
    }

    public Task Handle()
    {
        var fileName = GetDate() + ".json";
        var path = ConfigurePath(_filesPathSettings.GetPolygonDataFolderPath);

        using PolygonFileSaver saver = new PolygonFileSaver(path);

        bool saveResult = true;

        if (_plotManager.Polygons.Count == 0)
        {
            MessageBox.Show("Сохранить не удалось. Полигонов нет.", "Информация");
            return Task.CompletedTask;
        }

        try
        {
            saveResult = saver.Save(_plotManager.Polygons);
        }
        catch
        {
            saveResult = false;
        }

        saver.Close();

        if (saveResult)
            MessageBox.Show($"Сохранено в {fileName}.", "Информация");
        else
            MessageBox.Show("Сохранить не удалось.", "Информация");

        return Task.CompletedTask;
    }

    private string ConfigurePath(string path) =>
        Path.Combine(path, GetDate() + ".json");

    private string GetDate() => DateTime.Now.ToString("dd-MM-yyyy") + "_" + Guid.NewGuid().ToString();
}