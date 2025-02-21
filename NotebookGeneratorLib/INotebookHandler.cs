namespace WindowShared.Components.Notebooks;

public interface INotebookHandler
{
    void AddNotebooks(IEnumerable<VirtualNotebook> notebooks);
}