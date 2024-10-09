using Core.Models.Polygons;

namespace Core.Clippers;

public interface IClipper
{
	public List<Polygon> Clip(List<Polygon> polygons);

	public List<Polygon> Clip(Polygon polygon1, Polygon polygon2);
}
