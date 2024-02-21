using FSMS.Core.Models;

namespace FSMS.Core.Interfaces
{
    public interface IFileManagementService
    {
        void AddFile(string filename, string? shortcut = null);
        void RemoveFile(string shortcut);
        IEnumerable<FileModel> ListFiles();
        FileModel GetFileByShortcut(string shortcut);
        int GetTotalNumberOfFiles();
        long GetTotalSizeOfFiles();
    }
}