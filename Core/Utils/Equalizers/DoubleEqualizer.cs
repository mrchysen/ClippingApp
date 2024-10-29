namespace Core.Utils.Equalizers;

public class DoubleEqualizer : EqualizerBase<double>
{
	public DoubleEqualizer(double epsilon) : base(epsilon) { }

	public override bool IsEquals(double num1, double num2) => Math.Abs(num1 - num2) < Epsilon; 
}
