using Core.Models.Polygons;
using System.Text.Json;

namespace DAL.Files.Polygons;

public class PolygonFileLoader : IPolygonFileLoader
{
    protected StreamReader _reader;

    public PolygonFileLoader(string pathname)
    {
        _reader = new StreamReader(pathname);
    }

    public List<Polygon> Load()
    {
        List<Polygon>? result;

        try
        {
            result = JsonSerializer.Deserialize<List<Polygon>>(_reader.BaseStream);
        }
        catch 
        {
            return new();
        }

        if (result == null) return new();

        return result;
    }

    public void Dispose() => _reader.Dispose();

    public void Close() => _reader.Close();    
}
