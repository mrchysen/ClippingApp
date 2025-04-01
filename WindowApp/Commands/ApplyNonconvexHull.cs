using Application.PlotExtensions;
using Application.Randoms;
using Core.HullCreators.NoncovexAlgorithms;
using Core.HullCreators.QuickHull;
using Core.Models.Points;
using Core.Models.Polygons;
using WindowApp.Components.Plates.Hulls;
using WindowApp.Infrastructure;

namespace WindowApp.Commands;

public class ApplyNonconvexHull : IMainWindowCommand
{
    private PlotManager _plotManager;
    private HullsViewModel _viewModel;

    public ApplyNonconvexHull(
        PlotManager plotManager,
        HullsViewModel viewModel)
    {
        _viewModel = viewModel;
        _plotManager = plotManager;
    }

    public async Task Handle()
    {
        if (_plotManager.Points.Count <= 0)
            return;

        var task = Task.Run(() =>
        {
            Polygon polygon;
            IEnumerable<PointD> insidePoints;

            var quickHull = new PoogachevAlgorithm(_viewModel.NonconvexParameter);

            polygon = quickHull.CreateHull(_plotManager.Points);

            insidePoints = _plotManager.Points.Where(p => !polygon.Points.Contains(p));

            return (polygon, insidePoints);
        });

        var (polygon, insidePoints) = await task;

        _plotManager.DrawCurrentPolygon(polygon);
        _plotManager.Plot.AddMarkers(insidePoints.ToList());
    }
}
