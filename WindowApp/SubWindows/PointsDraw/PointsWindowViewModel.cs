using Core.Models.Points;
using System.Windows.Controls;
using System.Windows.Shapes;
using WindowApp.Utils;

namespace WindowApp.SubWindows.PointsDraw;

public class PointsWindowViewModel : ViewModelBase
{
    private int _pointCount = 10;

    public int PointCount
    {
        get => _pointCount;
        set
        {
            _pointCount = value;
            OnPropertyChanged();
        }
    }

    public PointsWindowViewModel() { }

    public PointsWindowViewModel(Canvas canvas)
    {
        Canvas = canvas;
    }

    public List<PointD> Points { get; set; } = new();

    public List<Ellipse> Ellipses { get; set; } = new();

    public Canvas Canvas { get; init; } = null!;
}
