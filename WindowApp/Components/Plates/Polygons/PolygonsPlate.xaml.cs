using Core.Clippers;
using Core.Clippers.ConvexPolygonClipper;
using Core.Clippers.RourkeChienPolygonClipper;
using Core.Clippers.WeilerAthertonPolygonClipper;
using System.Windows;
using System.Windows.Controls;
using WindowApp.Commands;
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

        _clipper = new ConvexPolygonClipper();
    }

    private void InitializeButtons()
    {
        DrawPolygonsButton.Click += async (o, e)
            => await new ShowPolygonDrawWindowCommand(PlotManager).Handle();
        NextPolygonsButton.Click += async (o, e)
            => await new PolygonExampleCommand(PlotManager).Handle();
        FindIntersectionButton.Click += async (o, e)
            => await new FindIntersectionCommand(PlotManager, _clipper).Handle();
    }

    private void ClipperAlgRadioButton_Checked(object sender, RoutedEventArgs e)
    {
        var radioButton = (RadioButton)sender;

        var clipType = radioButton.DataContext.ToString();

        _clipper = clipType switch
        {
            "0" => new ConvexPolygonClipper(),
            "1" => new RourkeChienPolygonClipper(),
            "2" => new WeilerAthertonPolygonClipper(),
            _ => new ConvexPolygonClipper()
        };
    }
}
