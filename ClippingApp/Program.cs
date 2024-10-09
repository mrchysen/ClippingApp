using Application.PolygonPlotting;
using Core.Clippers;
using Core.Colors;
using Core.Models.Polygons;
using System.Diagnostics;

var gen = new RandomPolygon();

List<Polygon> polygons = new List<Polygon>
{
	gen.Get(true, 4),
	gen.Get(true, 4)
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