using DI.Core;
using FSMS.Core.Helpers;
using FSMS.Core.Interfaces;
using FSMS.Services;

namespace FSMS.Starter;

public class ServiceConfigurator
{
    private static DiContainer _container;

    public static void ConfigureServices()
    {
        _container = new DiContainer();

        // Register all your services with the DI container here
        _container.Register<IStateManager, PersistenceHelper>(Scope.Singleton);
        _container.Register<IFileManagementService, FileManagementService>(Scope.Singleton);
        _container.Register<IProfileManager, ProfileManager>(Scope.Singleton);
    }

    public static T Resolve<T>()
    {
        return _container.Resolve<T>();
    }
}