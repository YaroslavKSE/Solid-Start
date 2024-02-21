using FSMS.Core.Events;
using FSMS.Core.Models;

namespace FSMS.Services.Events;

public class LimitReachedEventLogEntry : EventLogEntry
{
    public override string Name => "LimitReached";
    public LimitType LimitType { get; }


    public LimitReachedEventLogEntry(LimitType limitType)
    {
        LimitType = limitType;
    }

    public override Dictionary<string, object?> Params => new()
    {
        {"limitType", LimitType},
    };
}