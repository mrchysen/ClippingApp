using Core.Settings;
using Microsoft.Extensions.Options;

namespace WindowApp.Settings;

public class AccuracySettings : IAccuracySettings
{
    private readonly MathContstsSettings _mathContsts;
    public double GetAccuracy => _mathContsts.Accuracy;

    public AccuracySettings(IOptions<MathContstsSettings> mathContsts)
    {
        _mathContsts = mathContsts.Value;
    }
}
