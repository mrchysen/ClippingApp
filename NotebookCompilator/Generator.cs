using System.Text;

namespace NotebookCompilator;

public class Generator
{
    private string _path;
    private string _namespaceName;

    public Generator(string path, string namespaceName)
    {
        _path = path;
        _namespaceName = namespaceName;
    }

    public async Task Generate()
    {
        var startFileCode = await File.ReadAllTextAsync(
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "StartNotebookCode.txt"));
        var endFileCode = await File.ReadAllTextAsync(
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "EndNotebookCode.txt"));

        if (!Directory.Exists(_path))
        {
            Console.WriteLine($"No such path {_path}. Compilation failed");
            return;
        }

        Console.WriteLine("Start compilation in directory: " + _path);

        var files = Directory.GetFiles(_path);
        var filesNames = files.Select(files => Path.GetFileName(files));

        var fileInfos = (IEnumerable<(string Path, string FileName)>)files.Zip(filesNames);

        // Сохранить все файлы в формате <имя>.ncs

        int count = 0;
        foreach (var fileInfo in fileInfos)
        {
            var fileContent = await File.ReadAllTextAsync(fileInfo.Path);

            await File.WriteAllTextAsync(Path.Combine(_path, fileInfo.FileName.Replace(".cs", ".ncs")), fileContent);

            count++;
        }

        Console.WriteLine("Generated .ncs files: " + count);

        // Сгенерировать в старых .cs файлах код

        foreach (var fileInfo in fileInfos)
        {
            var fileContent = await File.ReadAllTextAsync(fileInfo.Path);

            StringBuilder sb = new StringBuilder(fileContent);

            sb.Replace("GeneratorMarkup.StartNotebook();", startFileCode)
              .Replace("<Name>", fileInfo.FileName.Replace(".cs", ""))
              .Replace("<Namespace>", _namespaceName)
              .Replace("GeneratorMarkup.EndNotebook();", endFileCode);

            await File.WriteAllTextAsync(fileInfo.Path, sb.ToString(), Encoding.UTF8);

            count++;
        }
    }

    public async Task Degenerate()
    {
        // удалить все .cs файлики
        Console.WriteLine("Start decompilation in directory: " + _path);

        var allFiles = Directory.GetFiles(_path); ;
        var ncsFiles = allFiles.Where(s => s.EndsWith(".ncs"));
        var csFiles = allFiles.Where(s => s.EndsWith(".cs"));

        foreach (var item in csFiles)
        {
            File.Delete(item);
        }
        Console.WriteLine("Deleted .cs files: " + csFiles.Count());

        // переименовать файлики .ncs -> .cs
        foreach (var filePath in ncsFiles)
        {
            var fileContent = await File.ReadAllTextAsync(filePath);

            await File.WriteAllTextAsync(filePath.Replace(".ncs", ".cs"), fileContent, Encoding.UTF8);

            File.Delete(filePath);
        }
        Console.WriteLine("Generated .cs files: " + ncsFiles.Count());
    }
}
