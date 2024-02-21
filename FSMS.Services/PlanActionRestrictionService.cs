using System.Text.Json;
using FSMS.Core.Interfaces;
using FSMS.Core.Models;

namespace FSMS.Services;

public class PlanActionRestrictionService : IPlanRestrictionChecker
{
    private const string ConfigFilesPath = "ConfigFiles/planActionRestrictions.json";
    private readonly PlanActionRestrictions _restrictions;

    public PlanActionRestrictionService()
    {
        var configJson = File.ReadAllText(ConfigFilesPath);
        _restrictions = JsonSerializer.Deserialize<PlanActionRestrictions>(configJson)
                        ?? new PlanActionRestrictions();
    }

    public bool IsActionAllowedForPlan(string actionName, string planName)
    {
        if (planName.Equals("Gold", StringComparison.OrdinalIgnoreCase))
        {
            return true; // All actions are allowed for Gold users.
        }
        return !_restrictions.GoldOnlyActions.Contains(actionName);
    }
}