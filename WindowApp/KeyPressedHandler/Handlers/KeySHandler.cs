using DAL.Files;
using DAL.Files.Polygons;
using Notification.Wpf;
using System.IO;

namespace WindowApp.KeyPressedHandler.Handlers;

// S = save
public class KeySHandler : KeyHandler
{
    public override async Task Handle(KeyHandlerObject obj)
    {
        await using PolygonFileSaver saver = new PolygonFileSaver(GetPath(obj));

        var saveResult = await saver.Save(obj.Polygons);

        saver.Close();

        var content = new NotificationContent
        {
            Title = "Сохранение",
            TrimType = NotificationTextTrimType.NoTrim
        };

        if (saveResult)
        {
            content.Message = "Сохранено успешно";
            content.Type = NotificationType.Success;
        }
        else
        {
            content.Message = "Не удалось сохранить";
            content.Type = NotificationType.Error;
        }
        
        obj.NotificationManager.Show(content, areaName: "WindowArea");
    }

    private string GetPath(KeyHandlerObject obj) => 
        Path.Combine(Directory.GetCurrentDirectory(), obj.FilePath + GetDate() + ".txt"); 

    private string GetDate() => DateTime.Now.ToString("dd-mm-yyyy");
}
