namespace FSMS.Core.Interfaces
{
    public interface IFileAction
    {
        bool CanHandle(string actionName, string filePath);
        void Execute(string filePath);
    }
}