using System.Drawing;

namespace Application.PolygonPlotting.Models;

public class FilePlotInfo
{
    public string Path { get; set; } = string.Empty;

    public Size PictureSize { get; set; }

    public double FileSize { get; set; }
}
