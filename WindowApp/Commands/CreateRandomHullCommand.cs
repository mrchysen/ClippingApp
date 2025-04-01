using Application.PlotExtensions;
using Application.Randoms;
using Core.HullCreators.QuickHull;
using Core.Models.Points;
using Core.Models.Polygons;
using WindowApp.Components.Plates.Hulls;
using WindowApp.Infrastructure;

namespace WindowApp.Commands;

public class CreateRandomHullCommand : IMainWindowCommand
{
    private readonly IPolygonGenerator _polygonGenerator;
    private PlotManager _plotManager;
    private HullsViewModel _viewModel;

    public CreateRandomHullCommand(
        PlotManager plotManager, 
        HullsViewModel viewModel,
        IPolygonGenerator? polygonGenerator = null)
    {
        _polygonGenerator = polygonGenerator ?? new RandomPolygonGenerator();
        _viewModel = viewModel;
        _plotManager = plotManager;
    }

    public async Task Handle()
    {
        var task = Task.Run(() =>
        {
            Polygon polygon;
            IEnumerable<PointD> insidePoints;

            var quickHull = new QuickHullAlgorithm();

            if (_plotManager.Points.Count > 0)
            {
                polygon = quickHull.CreateHull(_plotManager.Points);

                insidePoints = _plotManager.Points.Where(p => !polygon.Points.Contains(p));

                return (polygon, insidePoints);
            }

            List<PointD> points = _polygonGenerator.GeneratePoints(_viewModel.PointCount);

            polygon = quickHull.CreateHull(points);

            insidePoints = points.Where(p => !polygon.Points.Contains(p));

            return (polygon, insidePoints);
        });

        var (polygon, insidePoints) = await task;

        _plotManager.DrawCurrentPolygon(polygon, true);
        _plotManager.Plot.AddMarkers(insidePoints.ToList());
    }
}
