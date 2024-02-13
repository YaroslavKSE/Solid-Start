using System.Text.Json;
using FSMS.Core.Interfaces;
using FSMS.Core.Models;

namespace FSMS.Core.Helpers 
{
    public class PersistenceHelper : IStateManager
    {
        private const string StateFilesDirectory = "StateFiles";
        public PersistenceHelper()
        {
            // Ensure the directory exists
            if (!Directory.Exists(StateFilesDirectory))
            {
                Directory.CreateDirectory(StateFilesDirectory);
            }
        }

        public void SaveState(UserProfile profile)
        {
            var stateFilePath = GetStateFilePath(profile.ProfileName);
            var options = new JsonSerializerOptions {WriteIndented = true};
            var jsonString = JsonSerializer.Serialize(profile, options); // Serialize the entire profile
            File.WriteAllText(stateFilePath, jsonString);
        }

        public UserProfile LoadState(string profileName)
        {
            var stateFilePath = GetStateFilePath(profileName);
            if (!File.Exists(stateFilePath))
            {
                // Return a new profile with default settings if no saved state exists
                return new UserProfile { ProfileName = profileName, PlanName = "Basic", Files = new List<FileModel>() };
            }

            var jsonString = File.ReadAllText(stateFilePath);
            return JsonSerializer.Deserialize<UserProfile>(jsonString) ?? 
                   new UserProfile { ProfileName = profileName, PlanName = "Basic", Files = new List<FileModel>() };
        }
        
        // Generates a unique file path for each profile
        private string GetStateFilePath(string profileName)
        {
            var sanitizedProfileName = string.Join("_", profileName.Split(Path.GetInvalidFileNameChars()));
            // Save files within the StateFiles directory
            return Path.Combine(StateFilesDirectory, $"{sanitizedProfileName}_FileSystemsState.json");
        }
    }
}