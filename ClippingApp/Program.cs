using Core.ConvexHulls;
using Core.Models;

ConvexHullCreator c = new();

PointD p1 = new(1,1);

PointD p2 = new(10, 20);

PointD p3 = new(1,100);

Console.WriteLine(c.Orientation(p1, p2, p3));