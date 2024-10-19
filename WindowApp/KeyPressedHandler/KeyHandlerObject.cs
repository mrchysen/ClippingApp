using Core.Models.Polygons;
using Notification.Wpf;
using ScottPlot;
using ScottPlot.WPF;

namespace WindowApp.KeyPressedHandler;

public class KeyHandlerObject
{
    public WpfPlot UiPlot { get; set; } = null!;

    public Plot Plot { get; set; } = null!;

    public List<Polygon> Polygons { get; set; } = null!;

    public NotificationManager NotificationManager { get; set; } = null!;
}
