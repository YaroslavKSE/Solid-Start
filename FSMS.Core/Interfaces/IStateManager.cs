using FSMS.Core.Models;

namespace FSMS.Core.Interfaces
{
    public interface IStateManager
    {
        void SaveState(IEnumerable<FileModel> files, string profileName);
        IEnumerable<FileModel> LoadState(string profileName);
    }
}