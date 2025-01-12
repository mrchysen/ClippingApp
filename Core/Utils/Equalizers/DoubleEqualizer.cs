﻿using Core.Settings;

namespace Core.Utils.Equalizers;

public class DoubleEqualizer : IEqualizer<double>
{
    private readonly double _epsilon;

    public DoubleEqualizer(IAccuracySettings accuracySettings)
        => _epsilon = accuracySettings.GetAccuracy;

	public bool IsEquals(double num1, double num2) => Math.Abs(num1 - num2) < _epsilon; 
}
