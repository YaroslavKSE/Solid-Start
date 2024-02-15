using FSMS.Core.Interfaces;

namespace FSMS.Core.Helpers;
//make profile manager extensions
public static class ProfileManagerExtension
{
    public static bool EnsureLoggedIn(this IProfileManager profileManager)
    {
        if (profileManager.GetCurrentProfile() == null)
        {
            Console.WriteLine("No profile is currently active. Please login using the 'login' command.");
            return false;
        }
        return true;
    }

}