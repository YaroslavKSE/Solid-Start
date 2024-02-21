namespace FSMS.Core.Interfaces;

public interface IFileActionFactory
{
    IEnumerable<IFileAction> GetFileActions();
}