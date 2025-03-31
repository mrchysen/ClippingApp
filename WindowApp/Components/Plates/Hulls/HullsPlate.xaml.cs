using System.Windows.Controls;
using WindowApp.Commands;
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
        InitializeButtons();
    }

    private void InitializeButtons()
    {
        BuildConvexHullButton.Click += async (o, e) 
            => await new CreateRandomHullCommand(PlotManager, ViewModel).Handle();
        GenerateNonconvexHullButton.Click += async (o, e)
            => await new CreateRandomNonconvexHullCommand(PlotManager, ViewModel).Handle();
        BuildNonconvexHullButton.Click += async (o, e)
            => await new ApplyNonconvexHull(PlotManager, ViewModel).Handle();
        DrawPointsButton.Click += async (o, e)
            => await new ShowPointDrawWindowCommand(PlotManager).Handle();
    }
}
