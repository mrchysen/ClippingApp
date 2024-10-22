using Application.PolygonPlotting;
using Core.Models.Colors.Extensions;
using DAL.Files.Polygons;
using Microsoft.Win32;
using Notification.Wpf;
using System.IO;

namespace WindowApp.KeyPressedHandler.Handlers;

// B = Build from file
public class KeyBHandler : KeyHandler
{
    public override void Handle(KeyHandlerObject obj)
    {
        OpenFileDialog openFileDialog = new OpenFileDialog();

        openFileDialog.InitialDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, obj.FilePath);
        openFileDialog.DefaultExt = ".json";

        var result = openFileDialog.ShowDialog() ?? false;

        if (!result)
        {
            var warningContent = new NotificationContent
            {
                Title = "Ошибка",
                Message = "Не удалось выбрать файл",
                Type = NotificationType.Warning,
                TrimType = NotificationTextTrimType.NoTrim
            };

            obj.NotificationManager.Show(warningContent, areaName: "WindowArea");

            return;
        }

        var pathToFile = openFileDialog.FileName;

        using PolygonFileLoader loader = new(pathToFile);

        var list = loader.Load().RandomColors();

        loader.Close();

        obj.Polygons.Clear();
        obj.Polygons.AddRange(list);

        IPolygonArtist artist = new PolygonArtist(obj.Polygons);
        
        obj.Plot.Clear();
        artist.Plot(obj.Plot);
        obj.Plot.Axes.AutoScale();

        obj.UiPlot.Refresh();

        var content = new NotificationContent
        {
            Title = "Информация",
            Message = "Выбран файл " + openFileDialog.FileName,
            Type = NotificationType.Information,
            TrimType = NotificationTextTrimType.NoTrim
        };

        obj.NotificationManager.Show(content, areaName: "WindowArea");
    }
}
