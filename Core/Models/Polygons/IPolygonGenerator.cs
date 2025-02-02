using Core.Models.Points;

namespace Core.Models.Polygons;

public interface IPolygonGenerator
{
    Polygon Generate(bool sortByAngle, bool clockwise, int count);

    List<PointD> GeneratePoints(int count);
}
