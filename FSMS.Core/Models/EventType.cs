namespace FSMS.Core.Models;

public enum EventType
{
    UserLoggedIn,
    FileAdded,
    FileRemoved,
    FileActionInvoked,
    PlanChanged,
    LimitReached
}