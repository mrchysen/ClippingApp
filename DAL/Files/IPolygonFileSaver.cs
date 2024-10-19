using Core.Models.Polygons;

namespace DAL.Files;

public interface IPolygonFileSaver : IDisposable
{
    bool Save(List<Polygon> polygons);

    void Close();
}
