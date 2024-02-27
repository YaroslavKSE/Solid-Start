using FSMS.Core.Interfaces;
using FSMS.Core.Models;
using FSMS.Services.Events;
using FSMS.Services.Factories;

namespace FSMS.Services
{
    public class ProfileManager : IProfileManager
    {
        private readonly List<UserProfile> _profiles = new();
        private UserProfile _currentProfile;
        private readonly IStateManager _persistenceHelper;
        private readonly IEventLoggingService _eventLoggingService;

        public ProfileManager(IStateManager persistenceHelper, IEventLoggingService eventLoggingService)
        {
            _persistenceHelper = persistenceHelper;
            _eventLoggingService = eventLoggingService;
        }

        public void LoginOrCreateProfile(string profileName)
        {
            var profile = _profiles.FirstOrDefault(p => p.ProfileName == profileName);
            if (profile == null)
            {
                profile = new UserProfile {ProfileName = profileName, Files = new List<FileModel>()};
                // Load the profile's files from persistent storage
                var loadedFiles = _persistenceHelper.LoadState(profileName).Files;
                var profilePlan = _persistenceHelper.LoadState(profileName).PlanName;
                profile.Files.AddRange(loadedFiles);
                profile.PlanName = profilePlan;
                _profiles.Add(profile);
            }

            _currentProfile = profile;
            _eventLoggingService.LogEvent(new UserLoggedInEventLogEntry(profileName));
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

        public void ChangeUserPlan(string newPlanName)
        {
            var currentProfile = GetCurrentProfile();
            if (currentProfile == null)
            {
                Console.WriteLine("No profile is currently active.");
                return;
            }

            var newPlan = PlanFactory.CreatePlan(newPlanName);
            if (!CanChangeToPlan(currentProfile, newPlan))
            {
                Console.WriteLine($"Cannot change to plan '{newPlanName}'. It exceeds the new plan's limits.");
                return;
            }

            currentProfile.PlanName = newPlanName;
            Console.WriteLine($"Plan successfully changed to {newPlanName}.");
            // Don't forget to save the updated profile
            _persistenceHelper.SaveState(currentProfile);
            _eventLoggingService.LogEvent(new PlanChangedEventLogEntry(currentProfile.ProfileName,
                newPlanName));
        }

        private bool CanChangeToPlan(UserProfile profile, IPlan newPlan)
        {
            // Calculate total file size in MB
            long totalSizeInBytes = profile.Files.Sum(file =>
            {
                try
                {
                    return new FileInfo(file.Path).Length;
                }
                catch (FileNotFoundException)
                {
                    Console.WriteLine($"Warning: File '{file.Path}' not found.");
                    return 0L; // Consider files that can't be found as having no size, adjust as necessary
                }
            });

            long totalSizeInMb = totalSizeInBytes / (1024 * 1024);

            // Check against new plan limits
            if (profile.Files.Count > newPlan.MaxFiles || totalSizeInMb > newPlan.MaxStorageInMb)
            {
                return false; // Profile does not meet the constraints of the new plan
            }

            return true; // Profile meets the constraints
        }
    }
}