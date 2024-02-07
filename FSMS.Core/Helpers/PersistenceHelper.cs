using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using FSMS.Core.Interfaces;
using FSMS.Core.Models;

namespace FSMS.Core.Helpers 
{
    public class PersistenceHelper : IState
    {
        private const string StateFilePath = "FileSystemsState.json";

        public void SaveState(IEnumerable<FileModel> files)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            var jsonString = JsonSerializer.Serialize(files, options);
            File.WriteAllText(StateFilePath, jsonString);
        }

        public IEnumerable<FileModel> LoadState()
        {
            if (!File.Exists(StateFilePath))
            {
                return new List<FileModel>();
            }

            var jsonString = File.ReadAllText(StateFilePath);
            return JsonSerializer.Deserialize<IEnumerable<FileModel>>(jsonString) ?? new List<FileModel>();
        }
    }
}