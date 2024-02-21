namespace FSMS.Core.Models;

public class EventLogEntry
{
    public string Event { get; set; }
    public DateTimeOffset Timestamp { get; set; }
    public Dictionary<string, object?> Params { get; set; }
}