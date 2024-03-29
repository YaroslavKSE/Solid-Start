﻿using FSMS.Core.Models;

namespace FSMS.Core.Interfaces
{
    public interface IProfileManager
    {
        void LoginOrCreateProfile(string profileName);
        UserProfile? GetCurrentProfile();
        public IPlan GetCurrentPlan();
        void ChangeUserPlan(string newPlanName);
    }
}