using System.Windows;

namespace WindowShared.Components.Notebooks.NotebookElements;

public class NotebookButton : INotebookElement
{
    private string _buttonText;
    private Action<object, RoutedEventArgs> _buttonAction;

    public NotebookButton(string buttonText, Action<object, RoutedEventArgs> buttonAction)
    {
        _buttonText = buttonText;
        _buttonAction = buttonAction;
    }

    public void InsertToNotebook(INotebook notebook)
    {
        notebook.AddButton(_buttonText, _buttonAction);
    }
}
