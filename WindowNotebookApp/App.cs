using System.Windows;

namespace WindowNotebookApp;

public class App : System.Windows.Application
{
    private readonly MainWindow _mainWindow;

    public App(MainWindow mainWindow)
    {
        _mainWindow = mainWindow;
        ShutdownMode = ShutdownMode.OnExplicitShutdown;
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        _mainWindow.Show();
        base.OnStartup(e);
    }
}
