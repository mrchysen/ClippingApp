using System.Drawing;

namespace Core.Utils.Equalizers;

public class PointEqualizer : EqualizerBase<Point>
{
	public PointEqualizer(double epsilon) : base(epsilon) { }

	public override bool IsEquals(Point obj1, Point obj2)
		=> Math.Abs(obj1.X - obj2.X) < Epsilon && Math.Abs(obj1.Y - obj2.Y) < Epsilon;
}
