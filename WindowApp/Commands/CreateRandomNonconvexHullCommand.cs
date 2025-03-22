using Core.HullCreators.NoncovexAlgorithms;
using Core.Models.Points;
using Core.Models.Polygons;
using WindowApp.Infrastructure;
using Application.PlotExtensions;

namespace WindowApp.Commands;

public class CreateRandomNonconvexHullCommand : ICommand
{
    private readonly IPolygonGenerator _polygonGenerator;

    public CreateRandomNonconvexHullCommand(IPolygonGenerator polygonGenerator)
    {
        _polygonGenerator = polygonGenerator;
    }

    public async Task Handle(PlotManager plotManager)
    {
        var task = Task.Run(() =>
        {
            Polygon polygon;
            IEnumerable<PointD> insidePoints;

            var quickHull = new PoogachevAlgorithm(plotManager.MainWindowContext.HullParameter);

            if (plotManager.Points.Count > 0)
            {
                polygon = quickHull.CreateHull(plotManager.Points);

                insidePoints = plotManager.Points.Where(p => !polygon.Points.Contains(p));

                return (polygon, insidePoints);
            }

            List<PointD> points = _polygonGenerator.GeneratePoints(plotManager.MainWindowContext.PointCountInHull);

            polygon = quickHull.CreateHull(points);

            insidePoints = points.Where(p => !polygon.Points.Contains(p));

            return (polygon, insidePoints);
        }); 

        var (polygon, insidePoints) = await task;

        plotManager.Polygons.Clear();
        plotManager.Polygons.Add(polygon);

        plotManager.DrawCurrentPolygons();
        plotManager.Plot.AddMarkers(insidePoints.ToList());
    }
}
