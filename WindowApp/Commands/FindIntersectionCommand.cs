using Core.Clippers;
using Core.Clippers.BulkIntersections;
using Core.Models.Polygons;
using WindowApp.Infrastructure;

namespace WindowApp.Commands;

public class FindIntersectionCommand : IMainWindowCommand
{
    private PlotManager _plotManager;
    private IClipper _clipper;

    public FindIntersectionCommand(
        PlotManager plotManager,
        IClipper clipper)
    {
        _plotManager = plotManager;
        _clipper = clipper;
    }

    public Task Handle()
    {
        if (_plotManager.Polygons.Count < 1)
            return Task.CompletedTask;

        List<Polygon> polygons = new();
        List<bool> isNeedToDrawNumbers;

        polygons = new BulkIntersection(_clipper)
                .FindAllClips(_plotManager.Polygons)
                .Where(p => p.Count > 0)
                .ToList();

        isNeedToDrawNumbers = CreateIndicatorsArray(polygons.Count + _plotManager.Polygons.Count);

        _plotManager.DrawCurrentPolygons(polygons, 
            clearLastPolygons: false, 
            isNeedToDrawNumbers: isNeedToDrawNumbers);

        return Task.CompletedTask;
    }

    private List<bool> CreateIndicatorsArray(int count)
    {
        var indicators = new List<bool>() { false, false };

        indicators.AddRange(Enumerable.Range(0, count - 2).Select(p => true).ToList());

        return indicators;
    }
}
