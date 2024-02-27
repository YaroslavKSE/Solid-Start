using FSMS.Core.Interfaces;

namespace FSMS.Services.FileActions;

public abstract class FileActionBase : IFileAction
{
    private readonly string _supportedExtension;
    private readonly string _supportedAction;

    protected FileActionBase(string supportedAction, string supportedExtension)
    {
        _supportedAction = supportedAction;
        _supportedExtension = supportedExtension;
    }

    public bool CanHandle(string actionName, string filePath)
    {
        var extension = Path.GetExtension(filePath);
        return actionName.Equals(_supportedAction, StringComparison.OrdinalIgnoreCase) &&
               extension.Equals(_supportedExtension, StringComparison.OrdinalIgnoreCase);
    }

    public abstract void Execute(string filePath);
}