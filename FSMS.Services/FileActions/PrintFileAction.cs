using System.Text.Json;
using FSMS.Core.Attributes;
using FSMS.Core.Interfaces;

namespace FSMS.Services.FileActions;
[FileAction("print", ".txt")]
public class PrintTextFileAction : IFileAction
{
    public bool CanHandle(string actionName, string filePath)
    {
        var extension = Path.GetExtension(filePath);
        return actionName.Equals("print", StringComparison.OrdinalIgnoreCase) && 
               extension.Equals(".txt", StringComparison.OrdinalIgnoreCase);
    }

    public void Execute(string filePath)
    {
        Console.WriteLine(File.ReadAllText(filePath));
    }
}

[FileAction("print", ".csv")]
public class PrintCsvFileAction : IFileAction
{
    public bool CanHandle(string actionName, string filePath)
    {
        var extension = Path.GetExtension(filePath);
        return actionName.Equals("print", StringComparison.OrdinalIgnoreCase) && 
               extension.Equals(".csv", StringComparison.OrdinalIgnoreCase);
    }

    public void Execute(string filePath)
    {
        var lines = File.ReadAllLines(filePath);
        foreach (var line in lines)
        {
            var columns = line.Split(',');
            Console.WriteLine(string.Join("\t", columns));
        }
    }
}

[FileAction("print", ".json")]
public class PrintJsonFileAction : IFileAction
{

    public bool CanHandle(string actionName, string filePath)
    {
        var extension = Path.GetExtension(filePath);
        return actionName.Equals("print", StringComparison.OrdinalIgnoreCase) && 
               extension.Equals(".json", StringComparison.OrdinalIgnoreCase);
    }

    public void Execute(string filePath)
    {
        var jsonString = File.ReadAllText(filePath);
        var jsonDocument = JsonDocument.Parse(jsonString);
        Console.WriteLine(JsonSerializer.Serialize(jsonDocument, new JsonSerializerOptions { WriteIndented = true }));
    }
}