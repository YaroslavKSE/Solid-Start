using FSMS.Core.Interfaces;

namespace FSMS.Services
{
    public class TextFileAction : IFileAction
    {
        public void ViewInfo(string path)
        {
            var fileInfo = new FileInfo(path);
            Console.WriteLine($"File: {fileInfo.Name}, Size: {fileInfo.Length} bytes, Path: {fileInfo.FullName}");
        }

        public void Print(string path)
        {
            Console.WriteLine(File.ReadAllText(path));
        }

        public void Summarize(string path)
        {
            var content = File.ReadAllText(path);
            // Example logic for summarizing text files
            var wordCount = content.Split(new[] { ' ', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries).Length;
            Console.WriteLine($"Word Count: {wordCount}");
            // Add more summarization logic as needed
        }

        public void Validate(string path)
        {
            // Validation logic for text files might be minimal or not applicable
            Console.WriteLine("Validation not applicable for text files.");
        }
    }
}