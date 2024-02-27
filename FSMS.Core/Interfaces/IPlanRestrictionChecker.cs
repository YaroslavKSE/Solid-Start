namespace FSMS.Core.Interfaces;

public interface IPlanRestrictionChecker
{
    bool IsActionAllowedForPlan(string actionName, string planName);
}