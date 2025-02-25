using Core.Models.Points;
using Core.Models.Polygons;

namespace Core.HullCreators;

public interface INonconvexCreator
{
    public Polygon CreateHull(List<PointD> points);
}
