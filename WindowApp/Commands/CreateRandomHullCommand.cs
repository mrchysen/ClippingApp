using Application.PlotExtensions;
using Application.PolygonPlotting;
using Core.HullCreators.QuickHull;
using Core.Models.Points;
using Core.Models.Polygons;
using System.Diagnostics;
using WindowApp.Infrastructure;

namespace WindowApp.Commands;

public class CreateRandomHullCommand : ICommand
{
    private readonly IPolygonGenerator _polygonGenerator;

    public CreateRandomHullCommand(IPolygonGenerator polygonGenerator)
    {
        _polygonGenerator = polygonGenerator;
    }

    public void Handle(PlotManager plotManager)
    {
        var quickHull = new QuickHullAlgorithm();

        List<PointD> points = _polygonGenerator.GeneratePoints(plotManager.MainWindowContext.PointCountInHull);

        var polygon = quickHull.CreateHull(points);

        var insidePoints = points.Where(p => !polygon.Points.Contains(p));

        plotManager.Polygons.Clear();
        plotManager.Polygons.Add(polygon);

        plotManager.DrawCurrentPolygons();
        plotManager.Plot.AddMarkers(insidePoints.ToList());
    }
}
