using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using WindowApp;
using WindowApp.Extensions;
using Application.Randoms;

public class Program
{
    [STAThread]
    public static void Main(string[] args)
    {
        var builder = Host.CreateApplicationBuilder(args);

        builder.Services.AddSingleton<App>();
        builder.Services.AddSingleton<MainWindow>();
        builder.Services.AddKeyHandlers();
        builder.Services.AddCommands();
        builder.Services.AddCore();

        builder.Services.Configure<RandomSettings>(
            builder.Configuration.GetSection(key: "Random"));

        var host = builder.Build();

        var app = host.Services.GetService<App>();
        
        app?.Run();
    }
}