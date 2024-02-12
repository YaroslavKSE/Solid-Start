using FSMS.Core.Interfaces;

namespace FSMS.Core.Helpers;

public class CommandHelper
{
    public static bool EnsureLoggedIn(IProfileManager profileManager)
    {
        if (profileManager.GetCurrentProfile() == null)
        {
            Console.WriteLine("No profile is currently active. Please login using the 'login' command.");
            return false;
        }
        return true;
    }

}