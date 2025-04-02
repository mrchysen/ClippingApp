using Core.Clustering.Metrics;
using Core.Models.Points;
using System.Windows.Controls;
using System.Windows.Media;
using WindowApp.Commands;
using WindowApp.Components.Plates.Clustering;
using WindowApp.Infrastructure;

namespace WindowApp.Components.Plates;

public partial class ClusteringPlate : UserControl
{
    private IMetric<double, PointD> _metric;
    
    public PlotManager PlotManager { get; set; } = null!;

    protected ClusteringViewModel ViewModel => (ClusteringViewModel)DataContext;

    public ClusteringPlate()
    {
        InitializeComponent();
        InitializeButtons();

        Background = Resources["BackgroundColor"] as SolidColorBrush;

        _metric = PointDMetricsFactory.CreateMetric(Metric.Euclidean);
    }

    private void InitializeButtons()
    {
        ClusteringButton.Click += async (o, e)
            => await new ClusteringCommand(PlotManager, ViewModel, _metric).Handle();
        DrawPointsButton.Click += async (o, e)
            => await new ShowPointDrawWindowCommand(PlotManager).Handle();
        DrawConvexHullButton.Click += async (o, e)
            => await new CreateConvexHullsOnClustersCommand(PlotManager).Handle();
        DrawRectangularHullButton.Click += async (o, e)
            => await new CreateRectangularsOnClustersCommand(PlotManager).Handle();
        DrawEllipsesHullButton.Click += async (o, e)
            => await new CreateEllipsesOnClustersCommand(PlotManager).Handle();
    }

    private void RadioButton_Checked(object sender, System.Windows.RoutedEventArgs e)
    {
        var radioButton = (RadioButton)sender;

        var metricType = radioButton.DataContext.ToString();

        _metric = metricType switch
        {
            "0" => PointDMetricsFactory.CreateMetric(Metric.Euclidean),
            "1" => PointDMetricsFactory.CreateMetric(Metric.Chebyshev),
            "2" => PointDMetricsFactory.CreateMetric(Metric.Cos),
            _ => PointDMetricsFactory.CreateMetric(Metric.Euclidean)
        };
    }
}
