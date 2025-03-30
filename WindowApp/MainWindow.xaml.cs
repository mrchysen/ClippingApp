using Core.Clippers.ConvexPolygonClipper;
using Core.Models.Polygons;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.IO;
using System.Windows;
using System.Windows.Input;
using WindowApp.Commands;
using WindowApp.Components.Plates;
using WindowApp.Infrastructure;
using WindowApp.Settings;

namespace WindowApp;

public partial class MainWindow : Window
{
    private readonly IServiceProvider _serviceProvider;
    
    private PlotManager _plotManager;
    private MainWindowViewModel _context = new();

    public MainWindow(IServiceProvider serviceProvider, IOptions<FilesPathSettings> filePathSettingsOptions)
    {
        InitializeComponent();

        _serviceProvider = serviceProvider;
        _plotManager = new(Plot, _serviceProvider.GetService<ConvexPolygonClipper>()!, _context, new List<Polygon>());
        
        InitializeButtonsClick();
        InitializePlateDataContext();
        InitializeMenuItemClicks();
        EnsureDataFoldersExistence(filePathSettingsOptions.Value);

        DataContext = _context;
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

    private void InitializeButtonsClick()
    {
        // TODO добавить cancelaration token
        //NextPolygonsButton.Click += (o, e) =>
        //    _serviceProvider.GetRequiredService<KeyNHandler>().Handle(_plotManager);
        //InfoAboutPolygonsButton.Click += (o, e) =>
        //    _serviceProvider.GetRequiredService<KeyIHandler>().Handle(_plotManager);
        //BuildConvexHullButton.Click += async (o, e) =>
        //    await _serviceProvider.GetRequiredService<CreateRandomHullCommand>().Handle(_plotManager);
        //BuildNonconvexHullButton.Click += async (o, e) =>
        //    await _serviceProvider.GetRequiredService<CreateRandomNonconvexHullCommand>().Handle(_plotManager);
        //DrawPolygonsButton.Click += (o, e) =>
        //    _serviceProvider.GetRequiredService<ShowPolygonDrawWindowCommand>().Handle(_plotManager);
        //FindIntersectionButton.Click += (o, e) =>
        //    _serviceProvider.GetRequiredService<FindIntersectionCommand>().Handle(_plotManager);
        //SavePolygonButton.Click += (o, e) =>
        //    _serviceProvider.GetRequiredService<KeySHandler>().Handle(_plotManager);
        //OpenPolygonFromFileButton.Click += (o, e) =>
        //    _serviceProvider.GetRequiredService<KeyBHandler>().Handle(_plotManager);
        //OpenFolderButton.Click += (o, e) =>
        //    _serviceProvider.GetRequiredService<OpenFolderCommand>().Handle(_plotManager);
        //DrawPointsButton.Click += (o, e) => 
        //    _serviceProvider.GetRequiredService<ShowPointDrawWindowCommand>().Handle(_plotManager);
        //ClearPointsAndPolygonsButton.Click += (o, e) =>
        //    _serviceProvider.GetRequiredService<ClearPolygonsAndPointsCommand>().Handle(_plotManager);
        //ClusteringButton.Click += (o, e) => 
        //    _serviceProvider.GetRequiredService<ClusteringCommand>().Handle(_plotManager);
    }

    private void EnsureDataFoldersExistence(FilesPathSettings filePathSettings) 
        => Directory.CreateDirectory(filePathSettings.GetPolygonDataFolderPath);
   private void Window_Closed(object sender, EventArgs e) => App.Current.Shutdown();
}