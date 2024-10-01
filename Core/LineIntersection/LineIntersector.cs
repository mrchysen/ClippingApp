using Core.Models;
using Core.Utils.Equalizers;

namespace Core.LineIntersection;

public class LineIntersector
{
	public double Epsilon { get; set; } = 0.001d;

	public EqualizerBase<double> Equalizer { get; set; } = new DoubleEqualizer(0.001);

	public virtual PointD? GetIntersectionPoint(Line line1, Line line2)
	{
		double det = line1.A * line2.B - line2.A * line1.B;

		if (Math.Abs(det) < Epsilon) // Определитель нормалей = 0 => параллельны
			return null;
		
		double x = (line1.B * line2.C - line2.B * line1.C) / det;
		double y = (line1.C * line2.A - line2.C * line1.A) / det;

		bool online1 = ((Math.Min(line1.Point1.X, line1.Point2.X) < x || Equalizer.IsEquals(Math.Min(line1.Point1.X, line1.Point2.X), x))
			&& (Math.Max(line1.Point1.X, line1.Point2.X) > x || Equalizer.IsEquals(Math.Max(line1.Point1.X, line1.Point2.X), x))
			&& (Math.Min(line1.Point1.Y, line1.Point2.Y) < y || Equalizer.IsEquals(Math.Min(line1.Point1.Y, line1.Point2.Y), y))
			&& (Math.Max(line1.Point1.Y, line1.Point2.Y) > y || Equalizer.IsEquals(Math.Max(line1.Point1.Y, line1.Point2.Y), y)));

		bool online2 = ((Math.Min(line2.Point1.X, line2.Point2.X) < x || Equalizer.IsEquals(Math.Min(line2.Point1.X, line2.Point2.X), x))
			&& (Math.Max(line2.Point1.X, line2.Point2.X) > x || Equalizer.IsEquals(Math.Max(line2.Point1.X, line2.Point2.X), x))
			&& (Math.Min(line2.Point1.Y, line2.Point2.Y) < y || Equalizer.IsEquals(Math.Min(line2.Point1.Y, line2.Point2.Y), y))
			&& (Math.Max(line2.Point1.Y, line2.Point2.Y) > y || Equalizer.IsEquals(Math.Max(line2.Point1.Y, line2.Point2.Y), y)));

		if (online1 && online2)
			return new PointD(x, y);
		return null;
	}

	private bool OnLine(Line l, PointD p)
	{
		// Векторное произведение направляющего вектора
		var dx1 = l.Point2.X - l.Point2.Y;
		dy1 = y2 - y1;

		dx = p.X - l.Point1.X;
		dy = p.Y - l.Point1.Y;

		S = dx1 * dy - dx * dy1;
	}
}
