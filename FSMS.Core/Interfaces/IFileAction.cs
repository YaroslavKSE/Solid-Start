namespace FSMS.Core.Interfaces
{
    public interface IFileAction
    {
        void ViewInfo(string path);
        void Print(string path);
        void Summarize(string path);
        void Validate(string path);
    }
}