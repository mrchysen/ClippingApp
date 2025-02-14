using Application.PolygonPlotting;
using Core.Models.Colors.Extensions;
using Core.Models.Polygons;
using DAL.Files.Polygons;
using Microsoft.Extensions.Options;
using Microsoft.Win32;
using System.IO;
using System.Windows;
using WindowApp.Infrastructure;
using WindowApp.Settings;

namespace WindowApp.KeyPressedHandler.Handlers;

// B = Build from file
public class KeyBHandler : KeyHandler
{
    private readonly FilesPathSettings _settings;

    public KeyBHandler(IOptions<FilesPathSettings> options)
    {
        _settings = options.Value;
    }

    public override void Handle(PlotManager plotManager)
    {
        var pathToFile = AskForFile();
        
        if(string.IsNullOrWhiteSpace(pathToFile))
            return;

        var list = new PolygonFileLoader(pathToFile).Load().RandomColors();

        plotManager.Polygons.Clear();
        plotManager.Polygons.AddRange(list);

        plotManager.DrawCurrentPolygons();
    }

    private string AskForFile()
    {
        OpenFileDialog openFileDialog = new OpenFileDialog();

        openFileDialog.InitialDirectory = _settings.GetPolygonDataFolderPath;

        openFileDialog.DefaultExt = ".json";

        var result = openFileDialog.ShowDialog() ?? false;

        if (!result)
        {
            return String.Empty;
        }

        return openFileDialog.FileName;
    }
}
