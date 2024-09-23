using Application.PolygonPlotting;
using Core.Colors;
using Core.Models;
using System.Diagnostics;

List<Polygon> polygons = new List<Polygon>
{
	new Polygon
	{
		Points = new()
		{
			new(1,1),
			new(1,50),
			new(50, 1),
		},
		Color = RandomColor.Get()
	},
	new Polygon
	{
		Points = new()
		{
			new(75,98),
			new(100,4),
			new(168, 174),
		},
		Color = RandomColor.Get()
	}
};

IPolygonArtist artist = new PolygonArtist(polygons);

artist.Plot(new()
{
	Path = "C://Programming/Files/pic.png",
	PictureSize = new(800,900)
});

var psi = new ProcessStartInfo("C://Programming/Files/pic.png")
{
	UseShellExecute = true
};

Process.Start(psi);