using Core.Models.Polygons;

namespace DAL.Files;

public interface IPolygonFileLoader : IDisposable
{
    Task<List<Polygon>> Load();

    void Close();
}
