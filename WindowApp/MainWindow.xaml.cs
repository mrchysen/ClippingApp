using Core.Models.Polygons;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Input;
using WindowApp.Components.ButtonsPopupComponent;
using WindowApp.Components.Utils;
using WindowApp.Infrastructure;
using WindowApp.KeyPressedHandler;
using WindowApp.KeyPressedHandler.Handlers;
using WindowApp.SubWindows.Polygons;

namespace WindowApp;

public partial class MainWindow : Window
{
    private readonly IServiceProvider _serviceProvider;
    private readonly PlotManager _plotManager;
    private List<Polygon> _polygons;

    private DoubleClickHandler<ButtonsPopup> _doubleClickHandler; 

    public MainWindow(IServiceProvider serviceProvider, List<Polygon> polygons)
    {
        InitializeComponent();

        _serviceProvider = serviceProvider;
        _plotManager = new(Plot);
        _polygons = polygons;

        _doubleClickHandler = new((buttonPopup) => buttonPopup.Show(),
            ButtonsPopup,
            [Key.LeftShift, Key.RightShift],
            new TimeSpan(0, 0, 0, 0, 400));

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
    {
        KeyHandlerFactory.GetHandler(e, _serviceProvider)?.Handle(_plotManager);
    }

    private void Window_KeyDown(object sender, KeyEventArgs e)
    {
        _doubleClickHandler.Click(e);
    }

    private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
    {
        _serviceProvider.GetRequiredService<PolygonsWindow>().Close();
    }

    private void Window_Closed(object sender, EventArgs e)
    {
        App.Current.Shutdown();
    }
}