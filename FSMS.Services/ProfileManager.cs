using FSMS.Core.Interfaces;
using FSMS.Core.Models;

namespace FSMS.Services
{
    public class ProfileManager : IProfileManager
    {
        private readonly List<UserProfile> _profiles = new();
        private UserProfile _currentProfile;

        public void LoginOrCreateProfile(string profileName)
        {
            var profile = _profiles.FirstOrDefault(p => p.ProfileName == profileName);
            if (profile == null)
            {
                profile = new UserProfile { ProfileName = profileName };
                _profiles.Add(profile);
            }
            _currentProfile = profile;
        }

        public UserProfile GetCurrentProfile()
        {
            return _currentProfile ?? throw new InvalidOperationException("No profile is currently active. Please login.");
        }
    }
}