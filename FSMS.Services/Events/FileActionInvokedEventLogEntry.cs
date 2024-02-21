using FSMS.Core.Events;

namespace FSMS.Services.Events;

public class FileActionInvokedEventLogEntry : EventLogEntry
{
    public override string Name => "FileActionInvoked";
    public string Shortcut { get; }
    public string Action { get; }

    public FileActionInvokedEventLogEntry(string shortcut, string action)
    {
        Shortcut = shortcut;
        Action = action;
    }

    public override Dictionary<string, object?> Params => new()
    {
        {"shortcut", Shortcut},
        {"action", Action}
    };
}