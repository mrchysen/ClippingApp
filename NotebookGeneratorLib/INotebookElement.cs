using NotebookGeneratorLib;

namespace WindowShared.Components.Notebooks.NotebookElements;

public interface INotebookElement
{
    public void InsertToNotebook(INotebook notebook);
}
