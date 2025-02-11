using Core.Models.Points;
using System.Windows;

namespace WindowApp.Extensions;

public static class PointExtensions
{
    public static PointD ToPointD(this Point point) => new PointD(point.X, point.Y);
    public static Point ToWindowPoint(this PointD point) => new Point(point.X, point.Y);
}
