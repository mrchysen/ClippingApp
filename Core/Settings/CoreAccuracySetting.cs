namespace Core.Settings;

public class CoreAccuracySetting : IAccuracySettings
{
    private readonly double _accuracy;

    public CoreAccuracySetting(double? accuracy = null)
    {
        _accuracy = accuracy ?? 0.0001d;
    }

    public double GetAccuracy => _accuracy;
}
