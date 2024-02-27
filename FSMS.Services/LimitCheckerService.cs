using FSMS.Core.Interfaces;
using FSMS.Core.Models;
using FSMS.Services.Events;

namespace FSMS.Services;

public class LimitCheckerService : ILimitCheckerService
{
    private readonly IProfileManager _profileManager;
    private readonly IEventLoggingService _eventLoggingService;

    public LimitCheckerService(IProfileManager profileManager, IEventLoggingService eventLoggingService)
    {
        _profileManager = profileManager;
        _eventLoggingService = eventLoggingService;
    }

    public bool CanAddFile(string filename, string? shortcut, out string reason)
    {
        var currentProfile = _profileManager.GetCurrentProfile();
        var currentPlan = _profileManager.GetCurrentPlan();
        var currentProfileFiles = currentProfile.Files;
        long newFileSize = new FileInfo(filename).Length;
        long totalSizeAfterAdding = currentProfileFiles.Sum(file => new FileInfo(file.Path).Length) + newFileSize;

        // Check for max files limit
        if (currentProfileFiles.Count >= currentPlan.MaxFiles)
        {
            reason = "Cannot add file. Exceeds the plan's limit on the number of files.";
            _eventLoggingService.LogEvent(new LimitReachedEventLogEntry(LimitType.FilesAmount));
            return false;
        }

        // Check for storage limit
        if (totalSizeAfterAdding > currentPlan.MaxStorageInMb * 1024 * 1024)
        {
            reason = "Cannot add file. Exceeds the plan's storage limit.";
            _eventLoggingService.LogEvent(new LimitReachedEventLogEntry(LimitType.Storage));
            return false;
        }

        // Check if the file already exists
        if (currentProfileFiles.Any(f => f.Shortcut == (shortcut ?? filename)))
        {
            reason = "A file with this shortcut already exists.";
            return false;
        }

        reason = "";
        return true;
    }
}