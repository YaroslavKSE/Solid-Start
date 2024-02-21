using FSMS.Core.Attributes;
using FSMS.Core.Interfaces;

namespace FSMS.Services.FileActions;

[FileAction("summary", ".txt")]
public class SummarizeFileAction() : FileActionBase("summary", ".txt")
{
    public override void Execute(string filePath)
    {
        var content = File.ReadAllText(filePath);
        // Example logic for summarizing text files
        var wordCount = content.Split(new[] {' ', '\n', '\r'}, StringSplitOptions.RemoveEmptyEntries).Length;
        Console.WriteLine($"Word Count: {wordCount}");
        // Add more summarization logic as needed
    }
}