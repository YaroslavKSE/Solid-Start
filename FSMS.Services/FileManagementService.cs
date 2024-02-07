using FSMS.Core.Interfaces;
using FSMS.Core.Models;

namespace FSMS.Services
{
    public class FileManagementService : IFileManagementService
    {
        private readonly List<FileModel> _files;
        private readonly IState _persistenceHelper;

        public FileManagementService(IState persistenceHelper)
        {
            _persistenceHelper = persistenceHelper;
            _files = _persistenceHelper.LoadState().ToList();
        }
        

        public void AddFile(string filename, string shortcut = null)
        {
            // Check if the file already exists in the list
            if (_files.Any(f => f.Shortcut == (shortcut ?? filename)))
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
            _files.Add(file);
            _persistenceHelper.SaveState(_files);
            Console.WriteLine($"File added successfully: {file.Shortcut}");
        }

        public void RemoveFile(string shortcut)
        {
            var file = _files.FirstOrDefault(f => f.Shortcut == shortcut);
            if (file != null)
            {
                _files.Remove(file);
                Console.WriteLine($"File removed: {shortcut}");
            }
            else
            {
                Console.WriteLine("File not found.");
            }
            _persistenceHelper.SaveState(_files);
        }

        public IEnumerable<FileModel> ListFiles()
        {
            if (!_files.Any())
            {
                Console.WriteLine("No files added yet.");
            }
            return _files;
        }

        public FileModel GetFileByShortcut(string shortcut)
        {
            return _files.FirstOrDefault(f => f.Shortcut == shortcut);
        }
    }
}