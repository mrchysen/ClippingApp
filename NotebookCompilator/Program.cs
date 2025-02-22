using NotebookCompilator;

if(args.Length == 0)
{
    Console.WriteLine("No command was given");
    return;
}

var command = args[0];
var baseDir = AppDomain.CurrentDomain.BaseDirectory;
var path = Path.Combine(baseDir, "..", "..", "..", "..", "WindowNotebookApp", "Notebooks");
var generator = new Generator(path);

if(command == "--compile" || command == "-c")
{
    Console.WriteLine("Compile started");
    await generator.Generate();
}
else if(command == "--uncompile" || command == "-u")
{
    Console.WriteLine("Uncompile started");
    await generator.Degenerate();
}
else
{
    Console.WriteLine("Not supported command");
    return;
}

Console.WriteLine("Done");