using Core.Models.Polygons;

namespace DAL.Files;

public interface IPolygonFileSaver : IAsyncDisposable
{
    Task<bool> Save(List<Polygon> polygons);

    void Close();
}
