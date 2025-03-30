using Core.HullCreators.NoncovexAlgorithms;
using Core.Models.Points;
using Core.Models.Polygons;
using WindowApp.Infrastructure;
using Application.PlotExtensions;

namespace WindowApp.Commands;

public class CreateRandomNonconvexHullCommand : IMainWindowCommand
{
    private readonly IPolygonGenerator _polygonGenerator;
    private PlotManager _plotManager;

    public CreateRandomNonconvexHullCommand(PlotManager plotManager, IPolygonGenerator polygonGenerator)
    {
        _polygonGenerator = polygonGenerator;
        _plotManager = plotManager;
    }

    public async Task Handle()
    {
        var task = Task.Run(() =>
        {
            Polygon polygon;
            IEnumerable<PointD> insidePoints;

            var quickHull = new PoogachevAlgorithm(_plotManager.MainWindowContext.HullParameter);

            if (_plotManager.Points.Count > 0)
            {
                polygon = quickHull.CreateHull(_plotManager.Points);

                insidePoints = _plotManager.Points.Where(p => !polygon.Points.Contains(p));

                return (polygon, insidePoints);
            }

            List<PointD> points = _polygonGenerator.GeneratePoints(_plotManager.MainWindowContext.PointCountInHull);

            polygon = quickHull.CreateHull(points);

            insidePoints = points.Where(p => !polygon.Points.Contains(p));

            return (polygon, insidePoints);
        }); 

        var (polygon, insidePoints) = await task;

        _plotManager.Polygons.Clear();
        _plotManager.Polygons.Add(polygon);

        _plotManager.DrawCurrentPolygons();
        _plotManager.Plot.AddMarkers(insidePoints.ToList());
    }
}
