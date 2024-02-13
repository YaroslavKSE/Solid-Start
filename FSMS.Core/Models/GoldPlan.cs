using FSMS.Core.Interfaces;

namespace FSMS.Core.Models;

public class GoldPlan : IPlan
{
    public string Name => "Gold";
    public int MaxFiles => 100;
    public long MaxStorageInMb => 1024;
}