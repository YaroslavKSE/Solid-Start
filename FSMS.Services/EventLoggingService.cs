using FSMS.Core.Interfaces;
using FSMS.Core.Models;

namespace FSMS.Services;

public class EventLoggingService : IEventLoggingService
{
    private readonly IEventLogger _eventLogger;

    public EventLoggingService(IEventLogger eventLogger)
    {
        _eventLogger = eventLogger;
    }

    public void LogUserLoggedIn(string userName)
    {
        LogEvent(EventType.UserLoggedIn, new {user_name = userName});
    }

    public void LogFileAdded(string shortcut, string filetype)
    {
        LogEvent(EventType.FileAdded, new {shortcut, filetype});
    }

    public void LogFileRemoved(string shortcut, string filetype)
    {
        LogEvent(EventType.FileRemoved, new {shortcut, filetype});
    }

    public void LogFileActionInvoked(string shortcut, string action)
    {
        LogEvent(EventType.FileActionInvoked, new {shortcut, action});
    }

    public void LogPlanChanged(string userName, string planName)
    {
        LogEvent(EventType.PlanChanged, new {user_name = userName, plan_name = planName});
    }

    public void LogLimitReached(LimitType limitType)
    {
        LogEvent(EventType.LimitReached, new {limit_type = limitType.ToString()});
    }

    private void LogEvent(EventType eventType, object parameters)
    {
        var eventLogEntry = new EventLogEntry
        {
            Event = eventType.ToString(),
            Timestamp = DateTimeOffset.UtcNow,
            Params = parameters.GetType().GetProperties().ToDictionary
            (
                prop => prop.Name,
                prop => prop.GetValue(parameters)
            )
        };
        _eventLogger.LogEvent(eventLogEntry);
    }
}