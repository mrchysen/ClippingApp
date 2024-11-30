using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using WindowApp;
using System.Diagnostics;
using WindowApp.Extensions;
using Microsoft.Extensions.Options;
using Application.Randoms;
using Microsoft.Extensions.Configuration;
using WindowApp.SubWindows;
using WindowApp.SubWindows.Polygons;

public class Program
{
    [STAThread]
    public static void Main(string[] args)
    {
        var builder = Host.CreateApplicationBuilder(args);

        builder.Services.AddSingleton<App>();
        builder.Services.AddSingleton<MainWindow>();
        builder.Services.AddScoped<PolygonsWindow>();
        builder.Services.AddKeyHandlers();
        builder.Services.AddCore();

        builder.Services.Configure<RandomSettings>(
            builder.Configuration.GetSection(key: "Random"));

        var host = builder.Build();

        var app = host.Services.GetService<App>();
        
        app?.Run();
    }
}