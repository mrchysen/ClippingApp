using Core.Models.Polygons;
using Notification.Wpf;
using ScottPlot;
using System.Windows;
using System.Windows.Input;
using WindowApp.KeyPressedHandler;

namespace WindowApp;

public partial class MainWindow : Window
{
    private Plot _plot;
    private NotificationManager _notificationManager;
    private List<Polygon> _polygons;

    private const string SaveFilePath = @"Saves\Polygons";

    public MainWindow()
    {
        InitializeComponent();

        _plot = Plot.Plot;
        _polygons = new List<Polygon>();
        _notificationManager = new();
    }

    public void Plot_KeyDown(object o, KeyEventArgs e)
    {
        KeyHandlerFactory.GetHandler(e.Key)?.Handle(new KeyHandlerObject()
        {
            UiPlot = Plot,
            Plot = _plot,
            Polygons = _polygons,
            NotificationManager = _notificationManager,
            FilePath = SaveFilePath
        });
    }
}