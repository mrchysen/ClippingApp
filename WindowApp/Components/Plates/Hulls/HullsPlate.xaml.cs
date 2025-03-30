using System.Windows.Controls;
using WindowApp.Components.Plates.Hulls;
using WindowApp.Infrastructure;

namespace WindowApp.Components.Plates;

public partial class HullsPlate : UserControl
{
    public PlotManager PlotManager { get; set; } = null!;

    protected HullsViewModel ViewModel => (HullsViewModel)DataContext;

    public HullsPlate()
    {
        InitializeComponent();
    }

    private void InitializeButtons()
    {

    }

    
}
