namespace FSMS.Core.Interfaces;

public interface IFileActionExecutor
{
    void ExecuteFileAction(string actionName, string filePath, string shortcut, string planName);
}