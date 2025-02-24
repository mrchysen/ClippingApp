using NotebookCompilator; // --command <namespace> <path>

if (args.Length < 3)
{
    Console.WriteLine("No command was given");
    return 0;
}

var command = args[0];
var fileNamespace = args[1];
var currentPath = Environment.CurrentDirectory ?? ".";
var path = Path.Combine(currentPath, args[2]);
Console.WriteLine(path);
var generator = new Generator(path, fileNamespace);

if(command == "--compile" || command == "-c")
{
    Console.WriteLine("Compile started in " + path);
    await generator.Generate();
}
else if(command == "--uncompile" || command == "-u")
{
    Console.WriteLine("Uncompile started in " + path);
    await generator.Degenerate();
}
else
{
    Console.WriteLine("Not supported command");
    return 0;
}

Console.WriteLine("Done");

return 0;