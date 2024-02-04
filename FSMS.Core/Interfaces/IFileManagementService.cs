using System.Collections.Generic;
using FSMS.Core.Models;

namespace FSMS.Core.Interfaces
{
    public interface IFileManagementService
    {
        void AddFile(string filename, string shortcut = null);
        void RemoveFile(string shortcut);
        IEnumerable<FileModel> ListFiles();
    }
}