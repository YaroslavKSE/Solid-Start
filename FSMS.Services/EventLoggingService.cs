using FSMS.Core.Events;
using FSMS.Core.Interfaces;

namespace FSMS.Services;

public class EventLoggingService : IEventLoggingService
{
    // private readonly IEnumerable<IEventLogger> _loggers;
    private readonly IEventLogger _logger;

    public EventLoggingService(IEventLogger logger)
    {
        _logger = logger;
    }
    // public EventLoggingService(IEnumerable<IEventLogger> loggers)
    // {
    //     _loggers = loggers;
    // }

    // public void LogEvent(EventLogEntry e)
    // {
    //     foreach (var logger in _loggers)
    //     {
    //         logger.LogEvent(e);
    //     }
    // }
    public void LogEvent(EventLogEntry e)
    {
        _logger.LogEvent(e);
    }
}