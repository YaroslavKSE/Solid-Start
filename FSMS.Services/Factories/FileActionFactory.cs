using DI.Core;
using FSMS.Core.Interfaces;

namespace FSMS.Services.Factories;

public class FileActionFactory : IFileActionFactory
{
    private readonly DiContainer _container;

    public FileActionFactory(DiContainer container)
    {
        _container = container;
    }

    public IEnumerable<IFileAction> GetFileActions()
    {
        return _container.ResolveAll<IFileAction>();
    }
}