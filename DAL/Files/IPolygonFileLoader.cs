using Core.Models.Polygons;

namespace DAL.Files;

public interface IPolygonFileLoader
{
    List<Polygon> Load();
}
