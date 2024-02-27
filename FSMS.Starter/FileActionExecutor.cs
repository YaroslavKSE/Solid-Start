using FSMS.Core.Interfaces;
using FSMS.Services.Events;
using FSMS.Services.Factories;

public class FileActionExecutor : IFileActionExecutor
{
    private readonly FileActionFactory _fileActionFactory;
    private readonly IEventLoggingService _eventLoggingService;
    private readonly IPlanRestrictionChecker _planActionRestrictionService;

    public FileActionExecutor(FileActionFactory fileActionFactory, IEventLoggingService eventLoggingService,
        IPlanRestrictionChecker planActionRestrictionService)
    {
        _fileActionFactory = fileActionFactory;
        _eventLoggingService = eventLoggingService;
        _planActionRestrictionService = planActionRestrictionService;
    }

    public void ExecuteFileAction(string actionName, string filePath, string shortcut, string userPlan)
    {
        if (!_planActionRestrictionService.IsActionAllowedForPlan(actionName, userPlan))
        {
            Console.WriteLine("This action is restricted to certain plan users.");
            return;
        }

        var actionHandlers = _fileActionFactory.GetFileActions();

        bool actionExecuted = false;
        foreach (var handler in actionHandlers)
        {
            if (handler.CanHandle(actionName, filePath))
            {
                handler.Execute(filePath);
                actionExecuted = true;
                _eventLoggingService.LogEvent(new FileActionInvokedEventLogEntry(shortcut, actionName));
                break;
            }
        }

        if (!actionExecuted)
        {
            Console.WriteLine("Action not supported for this file type.");
        }
    }
}