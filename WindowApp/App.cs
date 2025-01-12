using System.Windows;

namespace WindowApp;

public class App : System.Windows.Application
{
    public const string ApplicationName = "ClippingApp";
    
    private readonly MainWindow _mainWindow;

    public App(MainWindow mainWindow)
    {
        _mainWindow = mainWindow;
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        _mainWindow.Show();
        base.OnStartup(e);
    }
}