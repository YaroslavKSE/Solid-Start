using FSMS.Core.Interfaces;
using FSMS.Core.Models;
using FSMS.Services.Events;

namespace FSMS.Services
{
    public class FileManagementService : IFileManagementService
    {
        private readonly IStateManager _persistenceHelper;

        private readonly ICurrentProfileProvider _profileProvider;
        private readonly IEventLoggingService _eventLoggingService;
        private readonly ILimitCheckerService _limitCheckerService;

        public FileManagementService(IStateManager persistenceHelper,
            IEventLoggingService eventLoggingService, ILimitCheckerService limitCheckerService,
            ICurrentProfileProvider profileProvider)
        {
            _persistenceHelper = persistenceHelper;
            _eventLoggingService = eventLoggingService;
            _limitCheckerService = limitCheckerService;
            _profileProvider = profileProvider;
        }


        public void AddFile(string filename, string? shortcut = null)
        {
            // Access the current profile's files

            var currentProfile = _profileProvider.GetCurrentProfile();
            var currentProfileFiles = _profileProvider.GetCurrentProfileFiles();

            if (!_limitCheckerService.CanAddFile(filename, shortcut, out string reason))
            {
                Console.WriteLine(reason);
                // Log the event based on the reason, if necessary
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
            _eventLoggingService.LogEvent(new FileAddedEventLogEntry(file.Shortcut, file.Filename));
            Console.WriteLine($"File added successfully: {file.Shortcut}");
        }

        public void RemoveFile(string shortcut)
        {
            var currentProfile = _profileProvider.GetCurrentProfile();
            var currentProfileFiles = _profileProvider.GetCurrentProfileFiles();
            var file = currentProfileFiles.FirstOrDefault(f => f.Shortcut == shortcut);
            if (file != null)
            {
                currentProfileFiles.Remove(file);
                Console.WriteLine($"File removed: {shortcut}");
                _eventLoggingService.LogEvent(new FileRemovedEventLogEntry(file.Shortcut, file.Filename));
            }
            else
            {
                Console.WriteLine("File not found.");
            }

            _persistenceHelper.SaveState(currentProfile);
        }

        public IEnumerable<FileModel> ListFiles()
        {
            var currentProfileFiles = _profileProvider.GetCurrentProfileFiles();
            return currentProfileFiles;
        }

        public FileModel GetFileByShortcut(string shortcut)
        {
            // Access the current profile's files
            var currentProfileFiles = _profileProvider.GetCurrentProfileFiles();
            var fileByShortcut = currentProfileFiles.FirstOrDefault(f => f.Shortcut == shortcut);
            return fileByShortcut;
        }

        public int GetTotalNumberOfFiles()
        {
            var currentProfileFiles = _profileProvider.GetCurrentProfileFiles();
            return currentProfileFiles.Count;
        }

        public long GetTotalSizeOfFiles()
        {
            var currentProfileFiles = _profileProvider.GetCurrentProfileFiles();
            // Assuming FileModel has a Size property in bytes
            return currentProfileFiles.Sum(file => file.Path.Length);
        }
    }
}