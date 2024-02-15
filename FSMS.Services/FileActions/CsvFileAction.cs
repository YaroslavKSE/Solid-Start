using FSMS.Core.Interfaces;

// Rework, because not single responsible
namespace FSMS.Services
{
    public class CsvFileAction : IFileAction
    {
        public void ViewInfo(string path)
        {
            var fileInfo = new FileInfo(path);
            Console.WriteLine($"File: {fileInfo.Name}, Size: {fileInfo.Length} bytes, Path: {fileInfo.FullName}");
        }

        public void Print(string path)
        {
            var lines = File.ReadAllLines(path);
            foreach (var line in lines)
            {
                var columns = line.Split(',');
                Console.WriteLine(string.Join("\t", columns));
            }
        }

        public void Summarize(string path)
        {
            Console.WriteLine("Summarise is not applicable for CSV files.");
        }

        public void Validate(string path)
        {
            try
            {
                var lines = File.ReadAllLines(path);
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
}