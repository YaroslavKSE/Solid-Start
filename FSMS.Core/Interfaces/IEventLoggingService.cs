using FSMS.Core.Models;

namespace FSMS.Core.Interfaces;

public interface IEventLoggingService
{
    void LogUserLoggedIn(string userName);
    void LogFileAdded(string shortcut, string filetype);
    void LogFileRemoved(string shortcut, string filetype);
    void LogFileActionInvoked(string shortcut, string action);
    void LogPlanChanged(string userName, string planName);
    void LogLimitReached(LimitType limitType);
}