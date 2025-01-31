using Core.Clippers.ConvexPolygonClipper;
using Core.Clippers.RourkeChienPolygonClipper;
using Core.Clippers.WeilerAthertonPolygonClipper;
using Core.Models.Polygons;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using ScottPlot;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WindowApp.Components.ButtonsPopupComponent;
using WindowApp.Components.Utils;
using WindowApp.Infrastructure;
using WindowApp.KeyPressedHandler;
using WindowApp.KeyPressedHandler.Handlers;
using WindowApp.Settings;
using WindowApp.SubWindows.Polygons;

namespace WindowApp;

public partial class MainWindow : Window
{
    private readonly IServiceProvider _serviceProvider;
    private readonly PlotManager _plotManager;

    private DoubleClickHandler<ButtonsPopup> _doubleClickHandler = null!; 

    public MainWindow(IServiceProvider serviceProvider, IOptions<FilesPathSettings> filePathSettingsOptions)
    {
        InitializeComponent();
        InitializeDoubleClickMenu();
        EnsureDataFoldersExistence(filePathSettingsOptions.Value);

        _serviceProvider = serviceProvider;
        _plotManager = new(Plot, new List<Polygon>(), _serviceProvider.GetService<ConvexPolygonClipper>()!);
    }

    private void EnsureDataFoldersExistence(FilesPathSettings filePathSettings) 
        => Directory.CreateDirectory(filePathSettings.GetPolygonDataFolderPath);
    
    private void InitializeDoubleClickMenu()
    {
        _doubleClickHandler = new((buttonPopup) => buttonPopup.Show(),
            ButtonsPopup,
            [Key.LeftShift, Key.RightShift],
            new TimeSpan(0, 0, 0, 0, 400));

        ButtonsPopup.AddButton("Правое окошко",
            () =>
            {
                RightArea.Width = (RightArea.Width.Value == 0) ? new GridLength(1, GridUnitType.Star) : new GridLength(0);
            });
        ButtonsPopup.AddButton("Следующие полигоны",
            () => _serviceProvider.GetRequiredService<KeyNHandler>().Handle(_plotManager));
        ButtonsPopup.AddButton("Сохранить",
            () => _serviceProvider.GetRequiredService<KeySHandler>().Handle(_plotManager));
        ButtonsPopup.AddButton("Загрузить",
            () => _serviceProvider.GetRequiredService<KeyBHandler>().Handle(_plotManager));
        ButtonsPopup.AddButton("Информация",
            () => _serviceProvider.GetRequiredService<KeyIHandler>().Handle(_plotManager));
    }

    public void Plot_KeyDown(object o, KeyEventArgs e) 
        => KeyHandlerFactory.GetHandler(e, _serviceProvider)?.Handle(_plotManager);

    private void Window_KeyDown(object sender, KeyEventArgs e) => _doubleClickHandler.Click(e);

    private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) 
        => _serviceProvider.GetRequiredService<PolygonsWindow>().Close();

    private void Window_Closed(object sender, EventArgs e) => App.Current.Shutdown();

    private void ClipperAlgRadioButton_Checked(object sender, RoutedEventArgs e)
    {
        if (_plotManager is null)
            return;

        var radioButton = (RadioButton)sender;

        var clipType = radioButton.DataContext.ToString();

        _plotManager.Clipper = clipType switch
        {
            "1" => _serviceProvider.GetRequiredService<ConvexPolygonClipper>(),
            "2" => _serviceProvider.GetRequiredService<RourkeChienPolygonClipper>(),
            "3" => _serviceProvider.GetRequiredService<WeilerAthertonPolygonClipper>(),
            _ => _serviceProvider.GetRequiredService<ConvexPolygonClipper>()
        };
    }
}