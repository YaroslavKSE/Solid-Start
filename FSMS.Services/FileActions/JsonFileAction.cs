using FSMS.Core.Interfaces;
using System.Text.Json;

namespace FSMS.Services
{
    public class JsonFileAction : IFileAction
    {
        public void ViewInfo(string path)
        {
            var fileInfo = new FileInfo(path);
            Console.WriteLine($"File: {fileInfo.Name}, Size: {fileInfo.Length} bytes, Path: {fileInfo.FullName}");        }

        public void Print(string path)
        {
            var jsonString = File.ReadAllText(path);
            var jsonDocument = JsonDocument.Parse(jsonString);
            Console.WriteLine(JsonSerializer.Serialize(jsonDocument, new JsonSerializerOptions { WriteIndented = true }));
        }

        public void Summarize(string path)
        {
            Console.WriteLine("Summarise is not applicable for JSON files.");
        }

        public void Validate(string path)
        {
            try
            {
                var jsonString = File.ReadAllText(path);
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
}