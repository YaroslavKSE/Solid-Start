using FSMS.Core.Interfaces;
using FSMS.Core.Models;

namespace FSMS.Services
{
    public class ProfileManager : IProfileManager
    {
        private readonly List<UserProfile> _profiles = new();
        private UserProfile _currentProfile;
        private readonly IStateManager _persistenceHelper;

        public ProfileManager(IStateManager persistenceHelper)
        {
            _persistenceHelper = persistenceHelper;
        }
        
        public void LoginOrCreateProfile(string profileName)
        {
            var profile = _profiles.FirstOrDefault(p => p.ProfileName == profileName);
            if (profile == null)
            {
                profile = new UserProfile { ProfileName = profileName, Files = new List<FileModel>() };
                // Load the profile's files from persistent storage
                var loadedFiles = _persistenceHelper.LoadState(profileName).ToList();
                profile.Files.AddRange(loadedFiles);
                _profiles.Add(profile);
            }
            _currentProfile = profile;
        }

        public UserProfile? GetCurrentProfile()
        {
            return _currentProfile;
        }
    }
}