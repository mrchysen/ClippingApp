namespace WindowShared.Components.Notebooks.NotebookElements;

public class NotebookTextBlock : INotebookElement
{
    private string _text;
    private int _fontSize;

    public NotebookTextBlock(string text, int fontSize)
    {
        _text = text;
        _fontSize = fontSize;
    }

    public void InsertToNotebook(INotebook notebook)
    {
        notebook.AddTextBlock(_text, _fontSize);
    }
}
