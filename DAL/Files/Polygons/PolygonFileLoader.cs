using Core.Models.Polygons;
using System.Text.Json;

namespace DAL.Files.Polygons;

public class PolygonFileLoader : IPolygonFileLoader
{
    private readonly string Path;

    public PolygonFileLoader(string pathname)
    {
        Path = pathname;
    }

    public List<Polygon> Load()
    {
        List<Polygon>? result;

        using var sr = new StreamReader(Path);

        try
        {
            result = JsonSerializer.Deserialize<List<Polygon>>(sr.BaseStream);
        }
        catch 
        {
            return new();
        }

        sr.Close();

        if (result == null) return new();

        return result;
    }  
}
