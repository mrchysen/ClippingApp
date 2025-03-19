using System.IO;

namespace WindowApp.Settings;

public class FilesPathSettings
{
    public string PolygonDataFolderName { get; set; } = "Polygons";

    public string DataFolderName { get; set; } = "Data";

    public string GetPolygonDataFolderPath => Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), 
        App.ApplicationName,
        DataFolderName, 
        PolygonDataFolderName);
}
