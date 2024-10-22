using DAL.Files;
using DAL.Files.Polygons;
using Notification.Wpf;
using System.IO;

namespace WindowApp.KeyPressedHandler.Handlers;

// S = save
public class KeySHandler : KeyHandler
{
    public override void Handle(KeyHandlerObject obj)
    {
        var path = GetPath(obj);
        var name = GetDate();

        using PolygonFileSaver saver = new PolygonFileSaver(path);

        var content = new NotificationContent
        {
            Title = "Сохранение",
            TrimType = NotificationTextTrimType.NoTrim
        };

        bool saveResult = true;

        try
        {
            saveResult = saver.Save(obj.Polygons);

            saver.Close();
        }
        catch
        {
            saveResult = false;
        }

        if (saveResult)
        {
            content.Message = $"Сохранено успешно, имя файла {name}";
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
        Path.Combine(obj.FilePath, GetDate() + ".json"); 

    private string GetDate() => DateTime.Now.ToString("dd-M-yyyy") + "_" + Guid.NewGuid().ToString();
}
