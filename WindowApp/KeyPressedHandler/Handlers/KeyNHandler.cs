using Application.PolygonPlotting;
using Core.Clippers;
using Core.Models.Polygons;
using Core.PointsOrderers;
using Notification.Wpf;
using ScottPlot;
using System.Drawing;

namespace WindowApp.KeyPressedHandler.Handlers;

// N = next
public class KeyNHandler : KeyHandler
{
    public override void Handle(KeyHandlerObject obj)
    {
        if (obj == null)
            return;

        var gen = new RandomPolygon();
        gen.Area = new Rectangle(0, 0, 50, 50);

        PointsOrdererByAngle orderer = new PointsOrdererByAngle();

        var poly = gen.Get(true, 3);

        var center = orderer.GetCenterMass(poly.Points);

        obj.Polygons.Clear();
        obj.Polygons.AddRange([
            poly
            ]);

        IClipper clipper = new ConvexPolygonClipper();

        obj.Polygons.AddRange(clipper.Clip(obj.Polygons));

        IPolygonArtist artist = new PolygonArtist(obj.Polygons);

        obj.Plot.Clear();
        artist.Plot(obj.Plot);
        obj.Plot.Axes.AutoScale();
        obj.Plot.Add.Marker(center.X, center.Y);

        obj.UiPlot.Refresh();

        var content = new NotificationContent
        {
            Title = "Новый график",
            Message = "График обновлён",
            Type = NotificationType.Information,
            TrimType = NotificationTextTrimType.NoTrim
        };

        obj.NotificationManager.Show(content, areaName: "WindowArea");
    }
}
