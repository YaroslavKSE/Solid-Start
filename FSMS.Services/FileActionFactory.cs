using System.IO;
using FSMS.Core.Interfaces;

namespace FSMS.Services
{
    public static class FileActionFactory
    {
        public static IFileAction GetFileAction(string filePath)
        {
            var extension = Path.GetExtension(filePath).ToLower();
            switch (extension)
            {
                case ".txt":
                    return new TextFileAction();
                case ".csv":
                    return new CsvFileAction();
                case ".json":
                    return new JsonFileAction();
                default:
                    return new DefaultFileAction();
            }
        }
    }
}