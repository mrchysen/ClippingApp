using Application.PolygonPlotting;
using Core.Models.Colors.Extensions;
using Core.Models.Polygons;
using DAL.Files.Polygons;
using Microsoft.Extensions.Options;
using Microsoft.Win32;
using System.IO;
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
        OpenFileDialog openFileDialog = new OpenFileDialog();

        openFileDialog.InitialDirectory = _settings.GetPolygonDataFolderPath;

        openFileDialog.DefaultExt = ".json";

        var result = openFileDialog.ShowDialog() ?? false;

        if (!result)
            return;
        
        var pathToFile = openFileDialog.FileName;

        using PolygonFileLoader loader = new(pathToFile);

        var list = loader.Load().RandomColors();

        loader.Close();

        plotManager.Polygons.Clear();
        plotManager.Polygons.AddRange(list);

        IPolygonArtist artist = new PolygonArtist(plotManager.Polygons);

        plotManager.Plot.Clear();
        artist.Plot(plotManager.Plot);
        plotManager.Plot.Axes.AutoScale();

        plotManager.WpfPlot.Refresh();
    }
}
