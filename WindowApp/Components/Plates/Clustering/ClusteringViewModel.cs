using WindowApp.Utils;

namespace WindowApp.Components.Plates.Clustering;

public class ClusteringViewModel : ViewModelBase
{
    private int _clusterCount = 2;

    public int ClusterCount
    {
        get => _clusterCount;
        set
        {
            _clusterCount = value;
            OnPropertyChanged();
        }
    }
}
