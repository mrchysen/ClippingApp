using Core.HullCreators.NoncovexAlgorithms;
using Core.Models.Points;
using Core.Models.Polygons;
using WindowApp.Infrastructure;
using Application.PlotExtensions;
using Application.Randoms;
using WindowApp.Components.Plates.Hulls;

namespace WindowApp.Commands;

public class CreateRandomNonconvexHullCommand : IMainWindowCommand
{
    private readonly IPolygonGenerator _polygonGenerator;
    private PlotManager _plotManager;
    private HullsViewModel _viewModel;

    public CreateRandomNonconvexHullCommand(
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

            var quickHull = new PoogachevAlgorithm(_viewModel.NonconvexParameter);

            List<PointD> points = _polygonGenerator.GeneratePoints(_viewModel.PointCount);

            _plotManager.Points.Clear();
            _plotManager.Points.AddRange(points);

            polygon = quickHull.CreateHull(points);

            insidePoints = points.Where(p => !polygon.Points.Contains(p));

            return (polygon, insidePoints);
        }); 

        var (polygon, insidePoints) = await task;

        _plotManager.DrawCurrentPolygon(polygon);
        _plotManager.Plot.AddMarkers(insidePoints.ToList());
    }
}
