using ScottPlot;
using System.Windows;
using WindowShared.Components.Notebooks.NotebookElements;

namespace WindowShared.Components.Notebooks;

public class VirtualNotebook : INotebook
{
    protected IList<INotebookElement> _elements;

    public VirtualNotebook(IList<INotebookElement>? elements = null)
    {
        _elements = elements ?? new List<INotebookElement>();
    }

    public IEnumerable<INotebookElement> Elements => _elements;

    public void AddButton(string buttonText, Action<object, RoutedEventArgs> buttonAction)
    {
        _elements.Add(new NotebookButton(buttonText, buttonAction));
    }

    public void AddElement(INotebookElement element) => _elements.Add(element);

    public void AddPlot(Plot plot)
    {
        _elements.Add(new NotebookPlot(plot));
    }

    public void AddTextBlock(string text, int fontSize = 32)
    {
        _elements.Add(new NotebookTextBlock(text, fontSize));
    }
}
