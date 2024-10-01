using Application.PolygonPlotting;
using Core.Colors;
using Core.Models;
using System.Diagnostics;
using System.Drawing;

PointD p1 = new PointD(1,2);
PointD p2 = new PointD(11,1);

Line line = new Line(p1, p2);

Console.WriteLine(line.A);
Console.WriteLine(line.B);
Console.WriteLine(line.C);
Console.WriteLine(line.A * line.Point1.X + line.B * line.Point1.Y);

//List<Polygon> polygons = new List<Polygon>
//{
//	new Polygon
//	{
//		Points = new()
//		{
//			new(1,1),
//			new(1,50),
//			new(50, 1),
//		},
//		Color = RandomColor.Get()
//	},
//	new Polygon
//	{
//		Points = new()
//		{
//			new(75,98),
//			new(100,4),
//			new(168, 174),
//		},
//		Color = RandomColor.Get()
//	}
//};

//IPolygonArtist artist = new PolygonArtist(polygons);

//artist.Plot(new()
//{
//	Path = "C://Programming/Files/pic.png",
//	PictureSize = new(800,900)
//});

//var psi = new ProcessStartInfo("C://Programming/Files/pic.png")
//{
//	UseShellExecute = true
//};

//Process.Start(psi);