namespace FSMS.Core.Interfaces;

public interface ILimitCheckerService
{
    bool CanAddFile(string filename, string? shortcut, out string reason);
}