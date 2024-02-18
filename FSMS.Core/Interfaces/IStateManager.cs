using FSMS.Core.Models;

namespace FSMS.Core.Interfaces
{
    public interface IStateManager
    {
        void SaveState(UserProfile profile);
        UserProfile LoadState(string profileName);
    }
}