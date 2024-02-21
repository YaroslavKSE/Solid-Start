using System.Reflection;
using DI.Core;
using FSMS.Core.Attributes;
using FSMS.Core.Helpers;
using FSMS.Core.Interfaces;
using FSMS.Services;
using FSMS.Services.Factories;

namespace FSMS.Starter;

public class ServiceConfigurator
{
    private static DiContainer _container;

    public ServiceConfigurator(DiContainer diContainer)
    {
        _container = diContainer;
    }

    public void ConfigureServices()
    {
        // Register all your services with the DI container
        _container.Register<IStateManager, PersistenceHelper>(Scope.Singleton);
        _container.Register<IEventLogger, JsonFileEventLogger>(Scope.Singleton);
        _container.Register<IEventLoggingService, EventLoggingService>(Scope.Singleton);
        _container.Register<IProfileManager, ProfileManager>(Scope.Singleton);
        _container.Register<IFileManagementService, FileManagementService>(Scope.Singleton);
        _container.Register<IFileActionExecutor, FileActionExecutor>(Scope.Singleton);
        _container.Register<IFileActionFactory, FileActionFactory>(Scope.Singleton);
    }

    public void RegisterFileActions()
    {
        // List to hold all discovered types across assemblies
        var fileActionTypes = new List<Type>();

        // Add types from the executing assembly
        fileActionTypes.AddRange(Assembly.GetExecutingAssembly().GetTypes()
            .Where(t => t.GetCustomAttributes<FileActionAttribute>(inherit: false).Any()));

        // Load and add types from the FSMS.Services assembly
        // Adjust "FSMS.Services" to the correct assembly name as needed
        var servicesAssembly = Assembly.Load("FSMS.Services");
        fileActionTypes.AddRange(servicesAssembly.GetTypes()
            .Where(t => t.GetCustomAttributes<FileActionAttribute>(inherit: false).Any()));

        foreach (var type in fileActionTypes)
        {
            _container.Register(type, type, Scope.Transient); // Adjust based on your DI setup
        }
    }

    public static T Resolve<T>()
    {
        return _container.Resolve<T>();
    }
}