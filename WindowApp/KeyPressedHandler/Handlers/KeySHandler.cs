using DAL.Files.Polygons;
using System.IO;
using Core.Models.Polygons;
using WindowApp.Infrastructure;
using WindowApp.Settings;
using Microsoft.Extensions.Options;

namespace WindowApp.KeyPressedHandler.Handlers;

// S = save
public class KeySHandler : KeyHandler
{
    private readonly SaverSettings _settings;
    private readonly List<Polygon> _polygons;

    public KeySHandler(IOptions<SaverSettings> options, List<Polygon> polygons)
    {
        _settings = options.Value;
        _polygons = polygons;
    }

    public override void Handle(PlotManager plotManager)
    {
        var path = ConfigurePath(_settings.SavingFolderPath);
        var name = GetDate();

        using PolygonFileSaver saver = new PolygonFileSaver(path);

        bool saveResult = true;

        try
        {
            saveResult = saver.Save(_polygons);

            saver.Close();
        }
        catch
        {
            saveResult = false;
        }
    }

    private string ConfigurePath(string path) => 
        Path.Combine(path, GetDate() + ".json"); 

    private string GetDate() => DateTime.Now.ToString("dd-M-yyyy") + "_" + Guid.NewGuid().ToString();
}
