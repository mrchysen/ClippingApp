using Core.Models;

namespace Core.Utils.Equalizers;

public class PointDEqualizer : EqualizerBase<PointD>
{
	public PointDEqualizer(double epsilon) : base(epsilon) { }

	public override bool IsEquals(PointD obj1, PointD obj2)
		=> Math.Abs(obj1.X - obj2.X) < Epsilon && Math.Abs(obj1.Y - obj2.Y) < Epsilon;
}
