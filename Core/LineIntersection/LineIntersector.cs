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

		var point = new PointD(x, y);

		if (PointOnLine(line1, point) && PointOnLine(line2, point))
			return point;

		return null;
	}

	private bool PointOnLine(Line l, PointD p)
	{
		// Векторное произведение
		var v1x = l.Point2.X - l.Point1.X;
		var v1y = l.Point2.Y - l.Point1.Y;

        var v2x = p.X - l.Point1.X;
        var v2y = p.Y - l.Point1.Y;

        var S = v1x * v2y - v2x * v1y;

		return Math.Abs(S) < Epsilon;
	}
}
