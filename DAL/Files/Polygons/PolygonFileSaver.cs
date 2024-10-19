using Core.Models.Polygons;
using System.Text.Json;

namespace DAL.Files.Polygons;

public class PolygonFileSaver : IPolygonFileSaver
{
    private StreamWriter _writer;

    public PolygonFileSaver(string filePath)
    {
        _writer = new(filePath);
    }

    public void Close() => _writer.Close();

    public void Dispose() => _writer.Dispose();

    public bool Save(List<Polygon> polygons)
    {
        if(polygons == null) return false;

        try
        {
            _writer.Write(JsonSerializer.Serialize(polygons));
        }
        catch
        {
            return false;
        }

        return true;
    }
}
