using FSMS.Core.Events;
using FSMS.Core.Models;

namespace FSMS.Core.Interfaces;

public interface IEventLoggingService
{
    void LogEvent(EventLogEntry e);
}