using FSMS.Core.Attributes;
using FSMS.Core.Interfaces;

namespace FSMS.Services.FileActions;

[FileAction("info", ".")]
public class ViewInfoFileAction : IFileAction
{
    // Since ViewInfo is applicable to all files, CanHandle always returns true for the "info" action.
    public bool CanHandle(string actionName, string filePath)
    {
        // This action is applicable to all files, so no extension check is necessary.
        // Just make sure the actionName matches.
        return actionName.Equals("info", StringComparison.OrdinalIgnoreCase);
    }

    public void Execute(string filePath)
    {
        var fileInfo = new FileInfo(filePath);
        Console.WriteLine($"File: {fileInfo.Name}, Size: {fileInfo.Length} bytes, Path: {fileInfo.FullName}");
    }
}