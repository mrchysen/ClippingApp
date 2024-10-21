using Application.PolygonPlotting;
using Core.Clippers;
using Core.Models.Polygons;
using Notification.Wpf;
using System.Drawing;

namespace WindowApp.KeyPressedHandler.Handlers;

// N = next
public class KeyNHandler : KeyHandler
{
    public override Task Handle(KeyHandlerObject obj)
    {
        if (obj == null)
            return Task.CompletedTask;

        var gen = new RandomPolygon();
        gen.Area = new Rectangle(0, 0, 50, 50);

        obj.Polygons.Clear();
        obj.Polygons.AddRange([
            gen.Get(true, 3),
            gen.Get(true, 3)
            ]);

        IClipper clipper = new ConvexPolygonClipper();

        obj.Polygons.AddRange(clipper.Clip(obj.Polygons));

        IPolygonArtist artist = new PolygonArtist(obj.Polygons);

        obj.Plot.Clear();
        artist.Plot(obj.Plot);
        obj.Plot.Axes.AutoScale();

        obj.UiPlot.Refresh();

        var content = new NotificationContent
        {
            Title = "Новый график",
            Message = "График обновлён",
            Type = NotificationType.Information,
            TrimType = NotificationTextTrimType.NoTrim
        };

        obj.NotificationManager.Show(content, areaName: "WindowArea");

        return Task.CompletedTask;
    }
}
