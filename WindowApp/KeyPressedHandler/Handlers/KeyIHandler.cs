using Core.PointsOrderers;
using WindowApp.Infrastructure;
using WindowApp.SubWindows.Polygons;

namespace WindowApp.KeyPressedHandler.Handlers;

// I = Info
public class KeyIHandler : KeyHandler
{
    private readonly PointsOrdererByAngle _pointsOrdererByAngle;
    private readonly PolygonsWindow _window;

    public KeyIHandler(
        PointsOrdererByAngle pointsOrdererByAngle,
        PolygonsWindow window)
    {
        _pointsOrdererByAngle = pointsOrdererByAngle;
        _window = window;
    }

    public override void Handle(PlotManager plotManager)
    {
        if (plotManager.Polygons.Count < 2)
        {
            return;
        }

        var polygonData1 = plotManager.Polygons[0].Points.Select(p => new PolygonData()
        {
            X = p.X,
            Y = p.Y
        }).ToList();

        var polygonData2 = plotManager.Polygons[1].Points.Select(p => new PolygonData()
        {
            X = p.X,
            Y = p.Y
        }).ToList();

        _window.Polygon1Data = polygonData1;
        _window.Polygon2Data = polygonData2;

        _window.ShowDialog();
    }
}
