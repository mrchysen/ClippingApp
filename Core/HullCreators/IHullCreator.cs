using Core.Models.Points;
using Core.Models.Polygons;

namespace Core.HullCreators;

public interface IHullCreator
{
    public Polygon CreateHull(List<PointD> points);
}
