using Core.Models.Polygons;

namespace DAL.Files;

public interface IPolygonFileLoader : IDisposable
{
    List<Polygon> Load();

    void Close();
}
