using FSMS.Core.Events;

namespace FSMS.Services.Events;

public class PlanChangedEventLogEntry : EventLogEntry
{
    public override string Name => "PlanChanged";
    public string Username { get; }
    public string PlanName { get; }

    public PlanChangedEventLogEntry(string username, string planName)
    {
        Username = username;
        PlanName = planName;
    }

    public override Dictionary<string, object?> Params => new()
    {
        {"username", Username},
        {"planName", PlanName}
    };
}