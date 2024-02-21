namespace FSMS.Core.Models
{
    public class UserProfile
    {
        public required string ProfileName { get; set; }
        public string PlanName { get; set; } = "Basic"; // Default to Basic plan
        public List<FileModel> Files { get; set; } = new();
    }
}