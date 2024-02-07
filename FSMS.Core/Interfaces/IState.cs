using FSMS.Core.Models;

namespace FSMS.Core.Interfaces
{
    public interface IState
    {
        void SaveState(IEnumerable<FileModel> files);
        IEnumerable<FileModel> LoadState();
    }
}