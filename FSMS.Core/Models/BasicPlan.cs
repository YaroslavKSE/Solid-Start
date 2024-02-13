using FSMS.Core.Interfaces;

namespace FSMS.Core.Models;

public class BasicPlan : IPlan
{
    public string Name => "Basic";
    public int MaxFiles => 10;
    public long MaxStorageInMb => 100;
}