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
                var loadedFiles = _persistenceHelper.LoadState(profileName).Files;
                profile.Files.AddRange(loadedFiles);
                _profiles.Add(profile);
            }
            _currentProfile = profile;
        }

        public UserProfile? GetCurrentProfile()
        {
            return _currentProfile;
        }

        public IPlan GetCurrentPlan()
        {
            var currentProfile = GetCurrentProfile();
            if (currentProfile == null)
            {
                throw new InvalidOperationException("No profile is currently active.");
            }

            return PlanFactory.CreatePlan(currentProfile.PlanName);
        }

        public void ChangePlan(string profileName, string newPlanName)
        {
            var profile = _profiles.FirstOrDefault(p => p.ProfileName == profileName);
            if (profile == null)
            {
                Console.WriteLine("Profile not found.");
                return;
            }

            // Example logic to check plan constraints before changing
            var newPlan = PlanFactory.CreatePlan(newPlanName);
            if (!CanChangeToPlan(profile, newPlan))
            {
                Console.WriteLine("Cannot change to the requested plan due to constraints.");
                return;
            }
        }
        private bool CanChangeToPlan(UserProfile profile, IPlan newPlan)
        {
            // Implement logic to check if the profile meets the constraints of the new plan
            // This could involve checking the number of files, total storage, etc.
            return true; // Placeholder
        }
    }
}