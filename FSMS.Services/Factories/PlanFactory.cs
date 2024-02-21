using FSMS.Core.Interfaces;
using FSMS.Core.Models;

namespace FSMS.Services.Factories;

public class PlanFactory
{
    public static IPlan CreatePlan(string planName)
    {
        return planName.ToLower() switch
        {
            "basic" => new BasicPlan(),
            "gold" => new GoldPlan(),
            _ => throw new ArgumentException("Invalid plan name", nameof(planName)),
        };
    }
}