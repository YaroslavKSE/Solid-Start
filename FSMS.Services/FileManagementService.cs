using FSMS.Core.Interfaces;
using FSMS.Core.Models;

namespace FSMS.Services
{
    public class FileManagementService : IFileManagementService
    {
        private readonly IStateManager _persistenceHelper;
        private readonly IProfileManager _profileManager;
        
        public FileManagementService(IStateManager persistenceHelper, IProfileManager profileManager)
        {
            _persistenceHelper = persistenceHelper;
            _profileManager = profileManager;
        }
        

        public void AddFile(string filename, string? shortcut = null)
        {
            // Access the current profile's files
            var currentProfile = _profileManager.GetCurrentProfile();
            var currentProfileFiles = currentProfile.Files;
            // Check if the file already exists in the list
            if (currentProfileFiles.Any(f => f.Shortcut == (shortcut ?? filename)))
            {
                Console.WriteLine("A file with this shortcut already exists.");
                return;
            }

            var file = new FileModel
            {
                Filename = Path.GetFileName(filename),
                Shortcut = shortcut ?? filename,
                Path = filename // Assuming full path is provided for simplicity
            };
            currentProfileFiles.Add(file);
            _persistenceHelper.SaveState(currentProfileFiles, currentProfile.ProfileName);
            Console.WriteLine($"File added successfully: {file.Shortcut}");
        }

        public void RemoveFile(string shortcut)
        {
            var currentProfile = _profileManager.GetCurrentProfile();
            var currentProfileFiles = currentProfile.Files;
            var file = currentProfileFiles.FirstOrDefault(f => f.Shortcut == shortcut);
            if (file != null)
            {
                currentProfileFiles.Remove(file);
                Console.WriteLine($"File removed: {shortcut}");
            }
            else
            {
                Console.WriteLine("File not found.");
            }
            _persistenceHelper.SaveState(currentProfileFiles, currentProfile.ProfileName);
        }

        public IEnumerable<FileModel> ListFiles()
        {
            var files = _profileManager.GetCurrentProfile().Files;
            // if (files.Count == 0)
            // {
            //     Console.WriteLine("No files added yet.");
            // }
            return files;
        }

        public FileModel GetFileByShortcut(string shortcut)
        {
            // Access the current profile's files
            return _profileManager.GetCurrentProfile().Files.FirstOrDefault(f => f.Shortcut == shortcut);
        }
    }
}