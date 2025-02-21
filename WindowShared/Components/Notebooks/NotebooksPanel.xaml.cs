using System.Windows.Controls;

namespace WindowShared.Components.Notebooks;

public partial class NotebooksPanel : TabControl, INotebookHandler
{
    public NotebooksPanel()
    {
        InitializeComponent();
    }

    public void AddNotebooks(IEnumerable<VirtualNotebook> notebooks)
    {
        var num = 1;
        foreach(var item in notebooks)
        {
            Items.Add(new TabItem()
            {
                Header = "Notebook " + num,
                Content = SimpleNotebook.CreateFromVirtual(item)
            });

            num++;
        }
    }
}
