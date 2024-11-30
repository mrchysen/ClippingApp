using Core.Models.Polygons;
using Core.PointsOrderers;
using System.Text;
using WindowApp.Infrastructure;
using WindowApp.SubWindows.Polygons;

namespace WindowApp.KeyPressedHandler.Handlers;

// I = Info
public class KeyIHandler : KeyHandler
{
    private readonly PointsOrdererByAngle _pointsOrdererByAngle;
    private readonly PolygonsWindow _window;
    private readonly List<Polygon> _polygons;

    public KeyIHandler(
        PointsOrdererByAngle pointsOrdererByAngle,
        PolygonsWindow window,
        List<Polygon> polygons)
    {
        _pointsOrdererByAngle = pointsOrdererByAngle;
        _window = window;
        _polygons = polygons;
    }

    public override void Handle(PlotManager plotManager)
    {
        if (!_polygons.Any())
        {
            return;
        }

        var polygonData1 = _polygons[0].Points.Select(p => new PolygonData()
        {
            X = p.X,
            Y = p.Y
        }).ToList();

        var polygonData2 = _polygons[1].Points.Select(p => new PolygonData()
        {
            X = p.X,
            Y = p.Y
        }).ToList();

        _window.Polygon1Data = polygonData1;
        _window.Polygon2Data = polygonData2;

        _window.ShowDialog();
    }

    private string GetInfo(List<Polygon> polygons)
    {
        StringBuilder sb = new();

        for (int i = 0; i < polygons.Count; i++)
        {
            sb.AppendLine($"{i+1}) {string.Join(" ", polygons[i].Points)}");
            sb.AppendLine($" {string.Join(" ", _pointsOrdererByAngle.GetAngels(polygons[i].Points))}");
        }   

        return sb.ToString();
    }
}
