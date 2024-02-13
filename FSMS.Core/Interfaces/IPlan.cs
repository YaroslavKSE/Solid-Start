namespace FSMS.Core.Interfaces;

public interface IPlan
{
    string Name { get; }
    int MaxFiles { get; }
    long MaxStorageInMb { get; }
}