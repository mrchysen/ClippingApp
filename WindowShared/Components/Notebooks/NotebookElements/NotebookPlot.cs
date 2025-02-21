using ScottPlot;

namespace WindowShared.Components.Notebooks.NotebookElements;

public class NotebookPlot : INotebookElement
{
    private Plot _plot;

    public NotebookPlot(Plot plot)
    {
        _plot = plot;
    }

    public void InsertToNotebook(INotebook notebook)
    {
        notebook.AddPlot(_plot);
    }
}
