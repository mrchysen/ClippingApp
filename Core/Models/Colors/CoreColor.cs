namespace Core.Models.Colors;

public class CoreColor
{
    public byte R { get; set; }
    public byte G { get; set; }
    public byte B { get; set; }
    public byte A { get; set; }

    public static CoreColor IntersectColors(CoreColor color1, CoreColor color2)
    {
        var r = (byte)(color1.R ^ color2.R);
        var g = (byte)(color1.G ^ color2.G);
        var b = (byte)(color1.B ^ color2.B);
        var a = (byte)(color1.A ^ color2.A);

        return new CoreColor { R = r, G = g, B = b, A = a };
    }
}
