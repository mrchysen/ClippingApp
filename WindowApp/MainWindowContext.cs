using System.ComponentModel;

namespace WindowApp;

public class MainWindowContext : INotifyPropertyChanged
{
    private int _pointCountInHull = 10;
    public int PointCountInHull
    {
        get { return _pointCountInHull; }
        set
        {
            _pointCountInHull = value;
            OnPropertyChanged(nameof(PointCountInHull));
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}