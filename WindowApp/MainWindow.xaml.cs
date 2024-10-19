using Application.PolygonPlotting;
using Core.Clippers;
using Core.Models.Polygons;
using ScottPlot;
using System.Drawing;
using System.Windows;
using System.Windows.Input;

namespace WindowApp;

public partial class MainWindow : Window
{
    private Plot _plot;

    public MainWindow()
    {
        InitializeComponent();

        _plot = Plot.Plot;

        ConfigureLegend();
    }

    public void Plot_KeyDown(object o, KeyEventArgs e)
    {
        if(e.Key == Key.N)
        {
            var gen = new RandomPolygon();
            gen.Area = new Rectangle(0,0,50,50);

            List<Polygon> polygons = new List<Polygon>
            {
                gen.Get(true, 8),
                gen.Get(true, 8)
            };
            IClipper clipper = new ConvexPolygonClipper();

            polygons.AddRange(clipper.Clip(polygons));

            IPolygonArtist artist = new PolygonArtist(polygons);

            _plot.Clear();
            _plot = artist.Plot(_plot);
            _plot.Axes.AutoScale();

            Plot.Refresh();
        }
    }

    private void ConfigureLegend()
    {


        _plot.ShowLegend();
    }
}