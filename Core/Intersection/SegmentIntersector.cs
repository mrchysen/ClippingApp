using Core.Models.Lines;
using Core.Models.Points;
using Core.Settings;
using Core.Utils.Equalizers;

namespace Core.Intersection;

// TODO: Вынести в интерфейс?
public class SegmentIntersector
{
    private readonly IAccuracySettings _accuracySettings;
    private readonly IEqualizer<double> _equalizer;

    private double _epsilon => _accuracySettings.GetAccuracy;
    
    public SegmentIntersector(IAccuracySettings? accuracySettings = null, IEqualizer<double>? equalizer = null)
    {
        _accuracySettings = accuracySettings ?? new CoreAccuracySetting();
        _equalizer = equalizer ?? new DoubleEqualizer();
    }

    public virtual PointD? GetIntersectionPoint(Line line1, Line line2)
    {
        double det = line1.A * line2.B - line2.A * line1.B;

        if (Math.Abs(det) < _epsilon) // Определитель нормалей = 0 => параллельны
            return null;

        double x = (line1.B * line2.C - line2.B * line1.C) / det;
        double y = (line1.C * line2.A - line2.C * line1.A) / det;

        var point = new PointD(x, y);

        // TODO: Сделать отдельный сервис, который проверяет принадлежность точки отрезку
		bool onSegment1 = ((Math.Min(line1.Point1.X, line1.Point2.X) < x || _equalizer.IsEquals(Math.Min(line1.Point1.X, line1.Point2.X), x))
			&& (Math.Max(line1.Point1.X, line1.Point2.X) > x || _equalizer.IsEquals(Math.Max(line1.Point1.X, line1.Point2.X), x))
			&& (Math.Min(line1.Point1.Y, line1.Point2.Y) < y || _equalizer.IsEquals(Math.Min(line1.Point1.Y, line1.Point2.Y), y))
			&& (Math.Max(line1.Point1.Y, line1.Point2.Y) > y || _equalizer.IsEquals(Math.Max(line1.Point1.Y, line1.Point2.Y), y)));

        bool onSegment2 = ((Math.Min(line2.Point1.X, line2.Point2.X) < x || _equalizer.IsEquals(Math.Min(line2.Point1.X, line2.Point2.X), x))
			&& (Math.Max(line2.Point1.X, line2.Point2.X) > x || _equalizer.IsEquals(Math.Max(line2.Point1.X, line2.Point2.X), x))
			&& (Math.Min(line2.Point1.Y, line2.Point2.Y) < y || _equalizer.IsEquals(Math.Min(line2.Point1.Y, line2.Point2.Y), y))
			&& (Math.Max(line2.Point1.Y, line2.Point2.Y) > y || _equalizer.IsEquals(Math.Max(line2.Point1.Y, line2.Point2.Y), y)));

		if (onSegment1 && onSegment2)
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

        return Math.Abs(S) < _epsilon;
    }
}
