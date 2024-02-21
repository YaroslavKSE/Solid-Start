using FSMS.Core.Events;

namespace FSMS.Core.Interfaces;

public interface IEventLogger
{
    void LogEvent(EventLogEntry eventLogEntry);
}