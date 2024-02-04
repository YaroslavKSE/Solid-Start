using FSMS.Core.Interfaces;
using FSMS.Services;

namespace FSMS.Starter;

public static class ServiceContainer
{
    public static IFileManagementService FileManagementService { get; private set; }

    static ServiceContainer()
    {
        // Initialize and register services here
        FileManagementService = new FileManagementService();
    }
}
