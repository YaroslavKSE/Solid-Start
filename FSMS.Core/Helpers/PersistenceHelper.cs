using System.Text.Json;
using FSMS.Core.Interfaces;
using FSMS.Core.Models;

namespace FSMS.Core.Helpers 
{
    public class PersistenceHelper : IState
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
        
        public void SaveState(IEnumerable<FileModel> files, string profileName)
        {
            var stateFilePath = GetStateFilePath(profileName);
            var options = new JsonSerializerOptions { WriteIndented = true };
            var jsonString = JsonSerializer.Serialize(files, options);
            File.WriteAllText(stateFilePath, jsonString);
        }

        public IEnumerable<FileModel> LoadState(string profileName)
        {
            var stateFilePath = GetStateFilePath(profileName);
            if (!File.Exists(stateFilePath))
            {
                return new List<FileModel>();
            }

            var jsonString = File.ReadAllText(stateFilePath);
            return JsonSerializer.Deserialize<IEnumerable<FileModel>>(jsonString) ?? new List<FileModel>();
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