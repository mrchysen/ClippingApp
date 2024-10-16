namespace Core.Utils.Equalizers;

public abstract class EqualizerBase<T>
{
    public double Epsilon { get; set; } = 0.01d;

    public EqualizerBase(double epsilon)
    {
        Epsilon = epsilon;
    }

    public abstract bool IsEquals(T obj1, T obj2);
}
