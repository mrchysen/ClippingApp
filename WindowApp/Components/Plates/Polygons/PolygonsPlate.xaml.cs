using Core.Clippers;
using System.Windows;
using System.Windows.Controls;
using WindowApp.Infrastructure;

namespace WindowApp.Components.Plates;

public partial class PolygonsPlate : UserControl
{
    private IClipper _clipper;
    public PlotManager PlotManager { get; set; } = null!;

    public PolygonsPlate()
    {
        InitializeComponent();
        InitializeButtons();
    }

    private void InitializeButtons()
    {

    }

    private void ClipperAlgRadioButton_Checked(object sender, RoutedEventArgs e)
    {
        var radioButton = (RadioButton)sender;

        var clipType = radioButton.DataContext.ToString();

        //_clipper = clipType switch
        //{
        //    "1" => _serviceProvider.GetRequiredService<ConvexPolygonClipper>(),
        //    "2" => _serviceProvider.GetRequiredService<RourkeChienPolygonClipper>(),
        //    "3" => _serviceProvider.GetRequiredService<WeilerAthertonPolygonClipper>(),
        //    _ => _serviceProvider.GetRequiredService<ConvexPolygonClipper>()
        //};
    }
}
