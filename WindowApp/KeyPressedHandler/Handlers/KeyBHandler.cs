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
    private readonly SaverSettings _settings;
    private readonly List<Polygon> _polygons;

    public KeyBHandler(IOptions<SaverSettings> options, List<Polygon> polygons)
    {
        _settings = options.Value;
        _polygons = polygons;
    }

    public override void Handle(PlotManager plotManager)
    {
        OpenFileDialog openFileDialog = new OpenFileDialog();

        openFileDialog.InitialDirectory = Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory,
            _settings.SavingFolderPath);

        openFileDialog.DefaultExt = ".json";

        var result = openFileDialog.ShowDialog() ?? false;

        if (!result)
            return;
        
        var pathToFile = openFileDialog.FileName;

        using PolygonFileLoader loader = new(pathToFile);

        var list = loader.Load().RandomColors();

        loader.Close();

        _polygons.Clear();
        _polygons.AddRange(list);

        IPolygonArtist artist = new PolygonArtist(_polygons);

        plotManager.Plot.Clear();
        artist.Plot(plotManager.Plot);
        plotManager.Plot.Axes.AutoScale();

        plotManager.WpfPlot.Refresh();
    }
}
