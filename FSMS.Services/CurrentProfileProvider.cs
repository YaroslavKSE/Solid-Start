using FSMS.Core.Interfaces;
using FSMS.Core.Models;

namespace FSMS.Services;

public class CurrentProfileProvider : ICurrentProfileProvider
{
    private readonly IProfileManager _profileManager;

    public CurrentProfileProvider(IProfileManager profileManager)
    {
        _profileManager = profileManager;
    }

    public UserProfile GetCurrentProfile()
    {
        return _profileManager.GetCurrentProfile();
    }

    public List<FileModel> GetCurrentProfileFiles()
    {
        return _profileManager.GetCurrentProfile().Files;
    }
}