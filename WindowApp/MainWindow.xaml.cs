using Microsoft.Extensions.Options;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Media;
using WindowApp.Commands;
using WindowApp.Components.Plates;
using WindowApp.Infrastructure;
using WindowApp.Settings;

namespace WindowApp;

public partial class MainWindow : Window
{
    private PlotManager _plotManager;

    public MainWindow(IOptions<FilesPathSettings> filePathSettingsOptions)
    {
        InitializeComponent();
        InitializeTheme();

        _plotManager = new(Plot);

        InitializePlateDataContext();
        InitializeMenuItemClicks();
        EnsureDataFoldersExistence(filePathSettingsOptions.Value);
    }

    private void InitializeTheme()
    {
        var backgroundBrush = Resources["BackgroundColor"] as SolidColorBrush;
        var fontBrush = Resources["FontColor"] as SolidColorBrush;

        Background = backgroundBrush;
        Plot.Plot.FigureBackground.Color =
            new($"#{backgroundBrush!.Color.R:X2}{backgroundBrush!.Color.G:X2}{backgroundBrush!.Color.B:X2}");
        Plot.Plot.Axes.Color(new($"#{fontBrush!.Color.R:X2}{fontBrush!.Color.G:X2}{fontBrush!.Color.B:X2}"));
    }

    private void InitializeMenuItemClicks()
    {
        SaveFileMenuItem.Click += (o, e) => new SaveFileCommand(_plotManager).Handle();
        OpenFileMenuItem.Click += (o, e) => new OpenFileCommand(_plotManager).Handle();
        OpenFolderMenuItem.Click += (o, e) => new OpenFolderCommand().Handle();

        ClearPlotMenuItem.Click += (o, e) => new ClearPlotCommand(_plotManager).Handle();

        ShowObjectsInfoMenuItem.Click += (o, e) => new ShowObjectsInfoCommand(_plotManager).Handle();
    }

    private void InitializePlateDataContext()
    {
        PolygonsPlate.PlotManager = _plotManager;
        HullsPlate.PlotManager = _plotManager;
        ClusteringPlate.PlotManager = _plotManager;
    }

    private void EnsureDataFoldersExistence(FilesPathSettings filePathSettings) 
        => Directory.CreateDirectory(filePathSettings.GetPolygonDataFolderPath);
   private void Window_Closed(object sender, EventArgs e) => App.Current.Shutdown();
}