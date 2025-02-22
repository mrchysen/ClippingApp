using WindowNotebookApp;

public static class ProgramNotebookApp
{
    [STAThread]
    public static void Main(string[] args)
    {
        App app = new App(new());

        app.Run();
    }
}