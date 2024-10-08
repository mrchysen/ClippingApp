using Application.PolygonPlotting;
using Core.Clippers;
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
			new(1,100),
			new(100, 1),
		},
		Color = RandomColor.Get()
	},
	new Polygon
	{
		Points = new()
		{
			new(25,25),
			new(50,25),
			new(25, 100),
		},
		Color = RandomColor.Get()
	}
};
IClipper clipper = new ConvexPolygonClipper();

polygons.AddRange(clipper.Clip(polygons));

IPolygonArtist artist = new PolygonArtist(polygons);

artist.Plot(new()
{
	Path = "C://Programming/Files/pic.png",
	PictureSize = new(800, 900)
});

var psi = new ProcessStartInfo("C://Programming/Files/pic.png")
{
	UseShellExecute = true
};

Process.Start(psi);