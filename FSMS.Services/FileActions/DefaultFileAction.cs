using FSMS.Core.Interfaces;

namespace FSMS.Services
{
    public class DefaultFileAction : IFileAction
    {
        public void ViewInfo(string path)
        {
            var fileInfo = new FileInfo(path);
            Console.WriteLine($"File: {fileInfo.Name}, Size: {fileInfo.Length} bytes, Path: {fileInfo.FullName}");
        }

        public void Print(string path)
        {
            Console.WriteLine($"Printing is not available for {Path.GetExtension(path).ToLower()} files.");
        }

        public void Summarize(string path)
        {
            Console.WriteLine($"Summarize is not available for {Path.GetExtension(path).ToLower()} files.");
        }

        public void Validate(string path)
        {
            Console.WriteLine($"Validation is not available for {Path.GetExtension(path).ToLower()} files.");
        }
    }
}