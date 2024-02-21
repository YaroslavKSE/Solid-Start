namespace FSMS.Core.Events;

public abstract class EventLogEntry
{
    public abstract string Name { get; }
    public DateTime Timestamp { get; } = DateTime.UtcNow;
    public abstract Dictionary<string, object?> Params { get; }
}