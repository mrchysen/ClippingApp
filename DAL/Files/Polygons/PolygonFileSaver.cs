using Core.Models.Polygons;
using System.Diagnostics;
using System.IO;
using System.Text.Json;

namespace DAL.Files.Polygons;

public class PolygonFileSaver : IPolygonFileSaver
{
    private StreamWriter _writer;

    public PolygonFileSaver(string filePath)
    {
        Directory.CreateDirectory(filePath);

        _writer = File.AppendText(filePath);
    }

    public void Close() => _writer.Close();

    public async ValueTask DisposeAsync() => await _writer.DisposeAsync();

    public async Task<bool> Save(List<Polygon> polygons)
    {
        if(polygons == null) return false;

        try
        {
            await _writer.WriteAsync(JsonSerializer.Serialize(polygons));
        }
        catch
        {
            return false;
        }

        return true;
    }
}
