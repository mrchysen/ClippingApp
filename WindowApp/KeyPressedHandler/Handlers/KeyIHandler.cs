using Core.Models.Polygons;
using Core.PointsOrderers;
using Notification.Wpf;
using System.Text;

namespace WindowApp.KeyPressedHandler.Handlers;

// I = Info
public class KeyIHandler : KeyHandler
{
    public override void Handle(KeyHandlerObject obj)
    {
        var content = new NotificationContent
        {
            Title = "Информация",
            Message = GetInfo(obj.Polygons),
            Type = NotificationType.Information,
            TrimType = NotificationTextTrimType.NoTrim
        };

        obj.NotificationManager.Show(content, areaName: "WindowArea");
    }

    private string GetInfo(List<Polygon> polygons)
    {
        StringBuilder sb = new();
        PointsOrdererByAngle orderer = new PointsOrdererByAngle();


        for (int i = 0; i < polygons.Count; i++)
        {
            sb.AppendLine($"{i+1}) {string.Join(" ", polygons[i].Points)}");
            sb.AppendLine($" {string.Join(" ", orderer.GetAngels(polygons[i].Points))}");
        }   

        return sb.ToString();
    }
}
