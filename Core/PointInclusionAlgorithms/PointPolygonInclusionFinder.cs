using Core.Models;
using Core.Models.Polygons;

namespace Core.PointInclusionAlgorithms;

public class PointPolygonInclusionFinder
{
    public bool CheckPointInsidePolygon(PointD point, Polygon polygon)
    {
        int i;
        int j;
        bool result = false;

        for (i = 0, j = polygon.Points.Count - 1; i < polygon.Points.Count; j = i++)
        {
            if ((polygon.Points[i].Y > point.Y) != (polygon.Points[j].Y > point.Y) &&
                (point.X < (polygon.Points[j].X - polygon.Points[i].X) * (point.Y - polygon.Points[i].Y) / (polygon.Points[j].Y - polygon.Points[i].Y) + polygon.Points[i].X))
            {
                result = !result;
            }
        }

        return result;
    }
}
