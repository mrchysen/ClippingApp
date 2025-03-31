using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using WindowApp;

public class Program
{
    [STAThread]
    public static void Main(string[] args)
    {
        var builder = Host.CreateApplicationBuilder(args);

        builder.Services.AddSingleton<App>();
        builder.Services.AddSingleton<MainWindow>();

        var host = builder.Build();

        var app = host.Services.GetService<App>();

        app?.Run();
    }
}