using FSMS.Core.Models;

namespace FSMS.Core.Interfaces;

public interface IEventLogger
{
    void LogEvent(EventLogEntry eventLogEntry);
}