using Core.Models.Colors;
using Core.Models.Points;
using Core.Models.Polygons;
using System.Windows.Media;

namespace WindowApp.SubWindows.Polygons;

class PolygonListViewModel
{
    public string Name { get; set; } = string.Empty;

    public List<PointD> Points { get; set; }

    public SolidColorBrush Fill { get; set; }

    public static PolygonListViewModel Create(List<PointD> points, CoreColor color, string polygonName) 
        => new PolygonListViewModel()
            {
                Name = polygonName,
                Points = points,
                Fill = new SolidColorBrush(Color.FromArgb(255, color.R, color.G, color.B))
            };
}
