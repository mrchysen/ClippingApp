using ScottPlot;
using ScottPlot.WPF;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WindowShared.Components.Notebooks;

public partial class SimpleNotebook : DockPanel, INotebook
{
    public SimpleNotebook()
    {
        InitializeComponent();
    }

    public static SimpleNotebook CreateFromVirtual(VirtualNotebook virtualNotebook)
    {
        var notebook = new SimpleNotebook();

        foreach(var el in virtualNotebook.Elements)
        {
            el.InsertToNotebook(notebook);
        }
    
        return notebook;
    }

    public void AddButton(string buttonText, Action<object, RoutedEventArgs> buttonAction)
    {
        var button = new Button()
        {
            Content = buttonText,
            Padding = new Thickness(10, 4, 10, 4),
            Width = 100
        };

        button.Click += new RoutedEventHandler(buttonAction);

        Notebook.Children.Add(button);
    }

    public void AddFromVirtualNotebook()
    {
        throw new NotImplementedException();
    }

    public void AddPlot(Plot plot)
    {
        var wpfPlot = new WpfPlot()
        {
            Height = 400,
        };
        wpfPlot.Reset(plot);

        Notebook.Children.Add(wpfPlot);
    }

    public void AddTextBlock(string text, int fontSize = 32)
    {
        var border = new Border()
        {
            Child = new TextBlock()
            {
                Margin = new Thickness(4),
                Text = text,
                FontSize = fontSize
            },
            Margin = new Thickness(4),
            BorderBrush = new SolidColorBrush(System.Windows.Media.Colors.Black),
            BorderThickness = new Thickness(1)
        };

        Notebook.Children.Add(border);
    }
}
