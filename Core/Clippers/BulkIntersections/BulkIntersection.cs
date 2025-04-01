using Core.Models.Colors.Extensions;
using Core.Models.Polygons;

namespace Core.Clippers.BulkIntersections;

public class BulkIntersection
{
    private IClipper _clipper;
    
    public BulkIntersection(IClipper clipper)
    {
        _clipper = clipper;
    }

    public List<Polygon> FindAllClips(List<Polygon> polygons)
    {
        List<Polygon> clippedPolygons = [];

        for (int i = 0; i < polygons.Count; i++)
        {
            for (int j = i + 1; j < polygons.Count; j++)
            {
                clippedPolygons.AddRange(_clipper.Clip(polygons[i], polygons[j]).RandomColors());
            }
        }

        return clippedPolygons;
    }
}
