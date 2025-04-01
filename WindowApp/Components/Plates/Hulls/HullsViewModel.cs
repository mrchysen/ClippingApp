using WindowApp.Utils;

namespace WindowApp.Components.Plates.Hulls;

public class HullsViewModel : ViewModelBase
{
    private int _nonconvexParameter = 2;

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

    public int NonconvexParameter
    {
        get => _nonconvexParameter;
        set
        {
            _nonconvexParameter = value;
            OnPropertyChanged();
        }
    }
}
