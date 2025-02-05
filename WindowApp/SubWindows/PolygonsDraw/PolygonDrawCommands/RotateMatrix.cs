using OpenTK.Mathematics;

namespace WindowApp.SubWindows.PolygonsDraw.PolygonDrawCommands;

public class RotateMatrix
{
    public static Matrix2 CreateMatrix(float angle) => new Matrix2()
    {
        M11 = MathF.Cos(angle),
        M12 = -MathF.Sin(angle),
        M21 = MathF.Sin(angle),
        M22 = MathF.Cos(angle)
    };
}
