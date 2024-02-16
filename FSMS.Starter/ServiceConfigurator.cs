using System.Reflection;
using DI.Core;
using FSMS.Core.Attributes;
using FSMS.Core.Helpers;
using FSMS.Core.Interfaces;
using FSMS.Services;

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
        _container.Register<IProfileManager, ProfileManager>(Scope.Singleton);
        _container.Register<IFileManagementService, FileManagementService>(Scope.Singleton);

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
            _container.Register(type, type, Scope.Singleton); // Adjust based on your DI setup
        }
    }
    
    public void ExecuteFileAction(string actionName, string filePath)
    {
        var actionHandlers = _container.ResolveAll<IFileAction>();

        bool actionExecuted = false;
        foreach (var handler in actionHandlers)
        {
            if (handler.CanHandle(actionName, filePath))
            {
                var executeMethod = handler.GetType().GetMethod("Execute");
                if (executeMethod != null)
                {
                    executeMethod.Invoke(handler, new object[] { filePath });
                    actionExecuted = true;
                    break;
                }
            }
        }

        if (!actionExecuted)
        {
            Console.WriteLine("Action not supported for this file type.");
        }
    }


    public static T Resolve<T>()
    {
        return _container.Resolve<T>();
    }
}