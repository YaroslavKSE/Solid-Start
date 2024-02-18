using FSMS.Core.Interfaces;
using FSMS.Services.Factories;

public class FileActionExecutor : IFileActionExecutor
{
    private readonly FileActionFactory _fileActionFactory;
    private readonly IEventLoggingService _eventLoggingService;

    public FileActionExecutor(FileActionFactory fileActionFactory, IEventLoggingService eventLoggingService)
    {
        _fileActionFactory = fileActionFactory;
        _eventLoggingService = eventLoggingService;
    }

    public void ExecuteFileAction(string actionName, string filePath, string shortcut)
    {
        var actionHandlers = _fileActionFactory.GetFileActions();

        bool actionExecuted = false;
        foreach (var handler in actionHandlers)
        {
            if (handler.CanHandle(actionName, filePath))
            {
                handler.Execute(filePath);
                actionExecuted = true;
                _eventLoggingService.LogFileActionInvoked(shortcut, actionName);
                break;
            }
        }

        if (!actionExecuted)
        {
            Console.WriteLine("Action not supported for this file type.");
        }
    }
}