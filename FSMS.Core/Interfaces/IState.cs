using FSMS.Core.Models;

namespace FSMS.Core.Interfaces
{
    public interface IState
    {
        void SaveState(IEnumerable<FileModel> files, string profileName);
        IEnumerable<FileModel> LoadState(string profileName);
    }
}