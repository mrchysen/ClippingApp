using DAL.Files.Polygons;
using System.IO;
using Core.Models.Polygons;
using WindowApp.Infrastructure;
using WindowApp.Settings;
using Microsoft.Extensions.Options;
using System.Windows;

namespace WindowApp.KeyPressedHandler.Handlers;

// S = save
public class KeySHandler : KeyHandler
{
    private readonly FilesPathSettings _filesPathSettings;

    public KeySHandler(IOptions<FilesPathSettings> options)
    {
        _filesPathSettings = options.Value;
    }

    public override void Handle(PlotManager plotManager)
    {
        var fileName = GetDate() + ".json";
        var path = ConfigurePath(_filesPathSettings.GetPolygonDataFolderPath);

        using PolygonFileSaver saver = new PolygonFileSaver(path);

        bool saveResult = true;

        if(plotManager.Polygons.Count == 0)
        {
            MessageBox.Show("Сохранить не удалось. Полигонов нет.", "Информация");
            return;
        }

        try
        {
            saveResult = saver.Save(plotManager.Polygons);
        }
        catch
        {
            saveResult = false;
        }

        saver.Close();

        if(saveResult) 
            MessageBox.Show($"Сохранено в {fileName}.", "Информация");
        else
            MessageBox.Show("Сохранить не удалось.", "Информация");
    }

    private string ConfigurePath(string path) => 
        Path.Combine(path, GetDate() + ".json"); 

    private string GetDate() => DateTime.Now.ToString("dd-MM-yyyy") + "_" + Guid.NewGuid().ToString();
}
