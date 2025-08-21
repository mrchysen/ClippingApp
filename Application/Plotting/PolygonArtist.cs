using Application.Converters.Polygons;
using Application.PlotExtensions;
using Core.Models.Colors;
using ScottPlot;
using System.Data;
using Polygon = Core.Models.Polygons.Polygon;

namespace Application.PolygonPlotting;

public class PolygonArtist : IPlotArtist
{
	protected List<Polygon> Polygons { get; set; } = new();
	private List<bool>? _isNeedToDrawPolygonsNumber;
	private bool _addMarkers = false;
	private bool _makeTransparent = true;

    public PolygonArtist(
		List<Polygon> polygons,
		bool addMarkers,
		bool makeTransparent,
        List<bool>? isNeedToDrawPolygonsNumber = null)
	{
		Polygons = polygons;
		_isNeedToDrawPolygonsNumber = isNeedToDrawPolygonsNumber;

		_addMarkers = addMarkers;
		_makeTransparent = makeTransparent;
    }

	public Plot Draw(Plot? plotInput = null)
	{
		var plot = plotInput ?? new();

		if (_makeTransparent)
		{
            Polygons = Polygons.Select(p =>
			{
				p.Color = new CoreColor()
				{
                    A = 126,
                    R = p.Color.R,
                    G = p.Color.G,
                    B = p.Color.B
                };

				return p;
			}).ToList();
        }

        var polygons = new PolygonConverter().ConvertListToScottPlot(Polygons);

        polygons.ForEach(plot.PlottableList.Add);

		// выглядит ужасно
		if (_addMarkers)
		{
			if(_isNeedToDrawPolygonsNumber is not null)
			{
                for (int i = 0; i < _isNeedToDrawPolygonsNumber.Count; i++)
                {
                    bool flag = _isNeedToDrawPolygonsNumber[i];

					if (flag)
					{
						plot.AddPolygonMarkersWithNumbers(Polygons[i]);
                    }
                }
			}
			else
			{
                plot.AddMarkersWithNumbers(Polygons);
            }
        }
		
        return plot;
    }
}
