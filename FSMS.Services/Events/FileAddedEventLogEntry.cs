using FSMS.Core.Events;

namespace FSMS.Services.Events;

public class FileAddedEventLogEntry : EventLogEntry
{
    public override string Name => "FileAdded";
    public string Shortcut { get; }
    public string FileType { get; }

    public FileAddedEventLogEntry(string shortcut, string fileType)
    {
        Shortcut = shortcut;
        FileType = fileType;
    }

    public override Dictionary<string, object?> Params => new()
    {
        {"shortcut", Shortcut},
        {"fileType", FileType}
    };
}