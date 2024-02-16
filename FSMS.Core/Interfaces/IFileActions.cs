namespace FSMS.Core.Interfaces
{
    public interface IViewInfoAction
    {
        void ViewInfo(string path);
    }

    public interface IPrintAction
    {
        void Print(string path);
    }

    public interface ISummarizeAction
    {
        void Summarize(string path);
    }

    public interface IValidateAction
    {
        void Validate(string path);
    }
}