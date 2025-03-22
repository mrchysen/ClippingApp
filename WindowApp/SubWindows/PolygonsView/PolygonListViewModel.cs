using Core.Models.Points;
using Core.Models.Polygons;
using System.Windows.Media;

namespace WindowApp.SubWindows.Polygons;

class PolygonListViewModel
{
    public string Name { get; set; } = string.Empty;

    public List<PointD> Points { get; set; }

    public SolidColorBrush Fill { get; set; }

    public static PolygonListViewModel Create(Polygon polygon, string polygonName) 
    {
        return new PolygonListViewModel() 
        {
            Name = polygonName,
            Points = polygon.Points,
            Fill = new SolidColorBrush(Color.FromArgb(255, polygon.Color.R, polygon.Color.G, polygon.Color.B)) 
        };
    }
}
