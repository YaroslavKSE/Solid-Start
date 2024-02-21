using FSMS.Core.Attributes;
using FSMS.Core.Interfaces;

namespace FSMS.Services.FileActions;

[FileAction("summary", ".txt")]
public class SummarizeFileAction : IFileAction
{
    public bool CanHandle(string actionName, string filePath)
    {
        var extension = Path.GetExtension(filePath);
        return actionName.Equals("summary", StringComparison.OrdinalIgnoreCase) &&
               extension.Equals(".txt", StringComparison.OrdinalIgnoreCase);
    }

    public void Execute(string filePath)
    {
        var content = File.ReadAllText(filePath);
        // Example logic for summarizing text files
        var wordCount = content.Split(new[] {' ', '\n', '\r'}, StringSplitOptions.RemoveEmptyEntries).Length;
        Console.WriteLine($"Word Count: {wordCount}");
        // Add more summarization logic as needed
    }
}