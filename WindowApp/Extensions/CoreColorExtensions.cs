using Core.Models.Colors;
using System.Windows.Media;

namespace WindowApp.Extensions;

public static class CoreColorExtensions
{
    public static Brush GetBrushColor(this CoreColor color) 
        => new SolidColorBrush(Color.FromArgb(255, color.R, color.G, color.B));
}
