using Core.Models.Polygons;
using DAL.Files;
using DAL.Files.Polygons;
using System.Diagnostics;

var gen = new RandomPolygon();

List<Polygon> polygons = new List<Polygon>
{
    gen.Get(true, 4),
    gen.Get(true, 4)
};

using IPolygonFileSaver saver = new PolygonFileSaver("file.txt");

saver.Save(polygons);

//IClipper clipper = new ConvexPolygonClipper();

//polygons.AddRange(clipper.Clip(polygons));

//IPolygonArtist artist = new PolygonArtist(polygons);

//artist.Plot(new FilePlotInfo()
//{
//	Path = "pic.png",
//	PictureSize = new(800, 800)
//});

var psi = new ProcessStartInfo("file.txt")
{
	UseShellExecute = true
};

Process.Start(psi);