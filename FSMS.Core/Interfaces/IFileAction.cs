namespace FSMS.Core.Interfaces
{
    // Rework, the action should apply to particular type
    public interface IFileAction
    {
        void ViewInfo(string path);
        void Print(string path);
        void Summarize(string path);
        void Validate(string path);
    }
}