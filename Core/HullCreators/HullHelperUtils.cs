using Core.Models.Points;

namespace Core.HullCreators;

public static class HullHelperUtils
{
    public static bool IsPointInTriangle(PointD A, PointD B, PointD C, PointD P)
    {
        double ABP = (B - A) * (P - A);
        double BCP = (C - B) * (P - B);
        double CAP = (A - C) * (P - C);

        return (ABP >= 0 && BCP >= 0 && CAP >= 0) || (ABP <= 0 && BCP <= 0 && CAP <= 0);
    }
}
