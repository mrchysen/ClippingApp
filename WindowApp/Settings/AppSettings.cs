using Application.Randoms;

namespace WindowApp.Settings;

public class AppSettings
{
    public MathContstsSettings MathContsts { get; set; } = null!;
    public SaverSettings Saver { get; set; } = null!;
    public RandomSettings Random { get; set; } = null!;
}
