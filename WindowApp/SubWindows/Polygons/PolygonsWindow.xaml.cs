using System.Windows;

namespace WindowApp.SubWindows.Polygons;

/// <summary>
/// Логика взаимодействия для PolygonsWindow.xaml
/// </summary>
public partial class PolygonsWindow : Window
{
    private List<PolygonData>? _polygon1Data;
    private List<PolygonData>? _polygon2Data;

    public List<PolygonData> Polygon1Data 
    {
        get => (List<PolygonData>)Polygon1Grid.ItemsSource;
        set
        {
            _polygon1Data = value;
            Polygon1Grid.ItemsSource = value;
        }
    }
    public List<PolygonData> Polygon2Data 
    {
        get => (List<PolygonData>)Polygon2Grid.ItemsSource;
        set
        {
            _polygon2Data = value;
            Polygon2Grid.ItemsSource = value;
        }
    }

    public PolygonsWindow()
    {
        InitializeComponent();
        CancelButton.Focus();
    }
        
    
    private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
    {
        e.Cancel = true;
        Visibility = Visibility.Hidden;
    }

    private void CancelButton_Click(object sender, RoutedEventArgs e)
        => Close();
}
