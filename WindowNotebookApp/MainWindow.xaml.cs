using Application.PolygonPlotting;
using Application.Randoms;
using Microsoft.Extensions.Options;
using ScottPlot;
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

        Notebooks.AddNotebooks([
                new VirtualNotebook([
                    new NotebookTextBlock("Привет!", 18),
                    new NotebookTextBlock("Привет!", 18),
                    new NotebookTextBlock("Привет!", 18),
                    new NotebookTextBlock("Привет!", 18),
                    new NotebookTextBlock("Привет!", 18),
                    new NotebookTextBlock("Привет!", 18),
                    new NotebookPlot(CreatePlotWithPolygon(5)),
                    new NotebookTextBlock("Привет!", 18),
                    new NotebookTextBlock("Привет!", 18),
                    new NotebookPlot(CreatePlotWithPolygon(4)),
                    new NotebookTextBlock("Привет!", 18),
                    new NotebookTextBlock("Привет!", 18),
                    new NotebookTextBlock("Привет!", 18),
                    new NotebookPlot(CreatePlotWithPolygon(3)),
                    new NotebookTextBlock("Привет!", 18),
                ]),
                new VirtualNotebook([
                    new NotebookTextBlock("Ку!", 18),
                    new NotebookPlot(CreatePlotWithPolygon(3)),
                    
                ]),
                new VirtualNotebook([
                    new NotebookTextBlock("Салам!", 18)
                ]),
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