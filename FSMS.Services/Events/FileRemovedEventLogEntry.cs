using FSMS.Core.Events;

namespace FSMS.Services.Events;

public class FileRemovedEventLogEntry : EventLogEntry
{
    public override string Name => "FileRemoved";
    public string Shortcut { get; }
    public string FileType { get; }

    public FileRemovedEventLogEntry(string shortcut, string fileType)
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