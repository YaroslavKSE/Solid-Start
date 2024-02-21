using System.Text.Json;
using FSMS.Core.Attributes;
using FSMS.Core.Interfaces;

namespace FSMS.Services.FileActions;

[FileAction("validate", ".csv")]
public class ValidateCsvFileAction() : FileActionBase("validate", ".csv")
{
    public override void Execute(string filePath)
    {
        try
        {
            var lines = File.ReadAllLines(filePath);
            if (lines.Any(line => line.Split(',').Length != lines[0].Split(',').Length))
            {
                Console.WriteLine("CSV validation failed: Inconsistent number of columns.");
            }
            else
            {
                Console.WriteLine("CSV format is valid.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"CSV validation failed: {ex.Message}");
        }
    }
}

[FileAction("validate", ".json")]
public class ValidateJsonFileAction() : FileActionBase("validate", ".json")
{
    public override void Execute(string filePath)
    {
        try
        {
            var jsonString = File.ReadAllText(filePath);
            JsonDocument.Parse(jsonString);
            Console.WriteLine("JSON format is valid.");
        }
        catch (JsonException)
        {
            Console.WriteLine("JSON validation failed: Invalid JSON format.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"JSON validation failed: {ex.Message}");
        }
    }
}