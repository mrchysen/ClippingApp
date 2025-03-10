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

    public void Handle(PlotManager plotManager)
    {
        var quickHull = new PoogachevAlgorithm(plotManager.MainWindowContext.HullParameter);

        List<PointD> points = _polygonGenerator.GeneratePoints(plotManager.MainWindowContext.PointCountInHull);

        var polygon = quickHull.CreateHull(points);

        var insidePoints = points.Where(p => !polygon.Points.Contains(p));

        plotManager.Polygons.Clear();
        plotManager.Polygons.Add(polygon);

        plotManager.DrawCurrentPolygons();
        plotManager.Plot.AddMarkers(insidePoints.ToList());
    }
}
