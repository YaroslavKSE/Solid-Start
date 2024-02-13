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
            var currentProfileFiles = currentProfile?.Files;
            
            var newFileSize = new FileInfo(filename).Length; // Get the size of the new file
            var totalSizeAfterAdding = GetTotalSizeOfFiles() + newFileSize;

            // Check for plan limits
            var currentPlan = _profileManager.GetCurrentPlan();
            if (GetTotalNumberOfFiles() >= currentPlan.MaxFiles || totalSizeAfterAdding > 
                currentPlan.MaxStorageInMb * 1024 * 1024)
            {
                Console.WriteLine("Cannot add file. Exceeds the plan's limit.");
                return;
            }
            
            // Check if the file already exists in the list
            if (currentProfileFiles != null && currentProfileFiles.Any(f => f.Shortcut == (shortcut ?? filename)))
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
            _persistenceHelper.SaveState(currentProfile);
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
            _persistenceHelper.SaveState(currentProfile);
        }

        public IEnumerable<FileModel> ListFiles()
        {
            var files = _profileManager.GetCurrentProfile().Files;
            return files;
        }

        public FileModel GetFileByShortcut(string shortcut)
        {
            // Access the current profile's files
            return _profileManager.GetCurrentProfile().Files.FirstOrDefault(f => f.Shortcut == shortcut);
        }

        public int GetTotalNumberOfFiles()
        {
            var currentProfile = _profileManager.GetCurrentProfile();
            return currentProfile.Files.Count;
        }

        public long GetTotalSizeOfFiles()
        {
            var currentProfile = _profileManager.GetCurrentProfile();
            // Assuming FileModel has a Size property in bytes
            return currentProfile.Files.Sum(file => file.Path.Length);
        }
    }
}