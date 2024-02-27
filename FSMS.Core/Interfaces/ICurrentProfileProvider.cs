using FSMS.Core.Models;

namespace FSMS.Core.Interfaces;

public interface ICurrentProfileProvider
{
    UserProfile GetCurrentProfile();
    List<FileModel> GetCurrentProfileFiles();
}