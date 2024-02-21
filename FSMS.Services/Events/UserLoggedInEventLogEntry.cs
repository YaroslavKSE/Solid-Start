using FSMS.Core.Events;

namespace FSMS.Services.Events;

public class UserLoggedInEventLogEntry : EventLogEntry
{
    public override string Name => "UserLoggedIn";
    public string UserName { get; }

    public UserLoggedInEventLogEntry(string userName)
    {
        UserName = userName;
    }

    public override Dictionary<string, object?> Params => new()
    {
        {"userName", UserName}
    };
}