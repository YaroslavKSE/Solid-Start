namespace FSMS.Core.Models
{
    public class UserProfile
    {
        public required string ProfileName { get; set; }
        public List<FileModel> Files { get; set; } = new();
    }
}