using WindowApp.Utils;

namespace WindowApp;

public class MainWindowViewModel : ViewModelBase
{
    private int _pointCountInHull = 10;

    private int _hullParameter = 2;

    private int _clusterCount = 2;

    public int ClusterCount
    {
        get { return _clusterCount; }
        set
        {
            _clusterCount = value;
            OnPropertyChanged(nameof(ClusterCount));
        }
    }

    public int PointCountInHull
    {
        get { return _pointCountInHull; }
        set
        {
            _pointCountInHull = value;
            OnPropertyChanged(nameof(PointCountInHull));
        }
    }

    public int HullParameter
    {
        get { return _hullParameter; }
        set
        {
            _hullParameter = value;
            OnPropertyChanged(nameof(HullParameter));
        }
    }
}