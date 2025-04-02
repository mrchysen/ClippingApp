using Core.Models.Colors;
using Core.Models.Points;
using System.Windows.Media;

namespace WindowApp.SubWindows.ObjectsInfo;

class ObjectsInfoViewModel
{
    public string Name { get; set; } = string.Empty;

    public ObjectType Type { get; set; }

    public List<PointD> Points { get; set; }

    public SolidColorBrush Fill { get; set; }

    public static ObjectsInfoViewModel Create(List<PointD> points, CoreColor color, string polygonName) 
        => new ObjectsInfoViewModel()
            {
                Name = polygonName,
                Points = points,
                Fill = new SolidColorBrush(Color.FromArgb(255, color.R, color.G, color.B))
            };
}

public enum ObjectType
{
    PointCloud,
    Centroid,
    Polygon,
    Cluster
}
