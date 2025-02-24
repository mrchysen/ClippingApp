using Application.PolygonPlotting;
using Application.Randoms;
using Microsoft.Extensions.Options;
using ScottPlot;
using System.Reflection;
using System.Windows;
using WindowShared.Components.Notebooks;
using WindowShared.Components.Notebooks.NotebookElements;

namespace WindowNotebookApp;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        var assembly = this.GetType().Assembly;

        Notebooks.AddNotebooks([ // ToDo do it more carefully with null possible type
                ((GeneratedNotebook)assembly.CreateInstance("WindowNotebookApp.Notebook1")).CreateNotebook()
            ]);
    }

    private Plot CreatePlotWithPolygon(int pointCount)
    {
        var generator = new RandomPolygonGenerator(new Random(), Options.Create(new RandomSettings()
        {
            Area = new RandomArea()
            {
                Height = 10,
                Width = 10
            }
        }));

        var artist = new PolygonArtist([generator.Generate(false, count: pointCount)]);

        return artist.Plot();
    }
}