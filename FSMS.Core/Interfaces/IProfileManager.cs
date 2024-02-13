using FSMS.Core.Models;

namespace FSMS.Core.Interfaces
{
    public interface IProfileManager
    {
        void LoginOrCreateProfile(string profileName);
        UserProfile? GetCurrentProfile();
        public IPlan GetCurrentPlan();
        void ChangePlan(string profileName, string newPlanName);
    }
}