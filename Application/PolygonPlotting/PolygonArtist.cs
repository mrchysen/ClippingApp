using Application.Converters.Polygons;
using Application.PlotExtensions;
using Application.PolygonPlotting.Models;
using Core.Models.Colors;
using ScottPlot;
using System.Data;
using Polygon = Core.Models.Polygons.Polygon;

namespace Application.PolygonPlotting;

public interface IPolygonArtist
{
    FilePlotInfo Plot(FilePlotInfo info);

    Plot Draw(Plot? plotInput, bool addMarkers = false, bool makeTransparent = true);
}

public class PolygonArtist : IPolygonArtist
{
	protected List<Polygon> Polygons { get; set; } = new();
	private List<bool>? _isNeedToDrawPolygonsNumber;

	public PolygonArtist(
		List<Polygon> polygons,
        List<bool>? isNeedToDrawPolygonsNumber = null)
	{
		Polygons = polygons;
		_isNeedToDrawPolygonsNumber = isNeedToDrawPolygonsNumber;

		if (_isNeedToDrawPolygonsNumber is not null 
			&& _isNeedToDrawPolygonsNumber.Count != Polygons.Count)
		{
			throw new InvalidOperationException("_isNeedToDrawPolygonsNumber.Count should be equal Polygons.Count");
		}
    }

    public FilePlotInfo Plot(FilePlotInfo info)
	{
		Plot plot = new();

		var polygons = new PolygonConverter().ConvertListToScottPlot(Polygons);

		polygons.ForEach(el => plot.PlottableList.Add(el));

		var plotInfo = plot.SavePng(
			info.Path, 
			info.PictureSize.Width, 
			info.PictureSize.Width);

		info.FileSize = plotInfo.FileSize;

		return info;
	}

	public Plot Draw(Plot? plotInput = null, bool addMarkers = true, bool makeTransparent = true)
	{
		var plot = plotInput ?? new();

		if (makeTransparent)
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

		if (addMarkers)
		{
			if(_isNeedToDrawPolygonsNumber is not null)
			{
                for (int i = 0; i < _isNeedToDrawPolygonsNumber.Count; i++)
                {
                    bool flag = _isNeedToDrawPolygonsNumber[i];

					if (flag)
					{
						plot.AddMarkersWithNumbers(Polygons[i]);
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
