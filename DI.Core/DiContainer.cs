namespace DI.Core;

public class DiContainer : IDiContainer
{
    private readonly Dictionary<Type, List<Binding>> _types = new();

    public void Register(Type interfaceType, Type implementationType, Scope scope)
    {
        if (!_types.ContainsKey(interfaceType))
        {
            _types[interfaceType] = new List<Binding>();
        }

        _types[interfaceType].Add(new Binding
        {
            ImplementationType = implementationType,
            Scope = scope
        });
    }


    public void Register<TInterface, TImplementation>(Scope scope)
    {
        Register(typeof(TInterface), typeof(TImplementation), scope);
    }

    public T Resolve<T>()
    {
        return (T)Resolve(typeof(T));
    }

    public T Instantiate<T>(Type type)
    {
        var constructor = type.GetConstructors().First();
        var parameters = new List<object>();
        foreach (var parameter in constructor.GetParameters())
        {
            parameters.Add(Resolve(parameter.ParameterType));
        }

        return (T)constructor.Invoke(parameters.ToArray());
    }

    
    public object Resolve(Type type)
    {
        return ResolveInternal(type, type, new List<Type>());
    }
    
    private object ResolveInternal(Type originalType, Type interfaceType, List<Type> resolutionsChain)
    {
        var binding = _types[interfaceType].First();
        
        if (binding.ImplementationObject != null)
        {
            return binding.ImplementationObject;
        }

        if (resolutionsChain.Contains(interfaceType))
        {
            throw new Exception(
                $"Cyclic dependency found during resolution of {originalType}: {string.Join(",", resolutionsChain.Select(t => t.Name))}. " +
                $"Got {interfaceType} again");
        }
        resolutionsChain.Add(interfaceType);

        var constructor = binding.ImplementationType.GetConstructors().First();
        var parameters = new List<object>();
        foreach (var parameter in constructor.GetParameters())
        {
            parameters.Add(ResolveInternal(originalType, parameter.ParameterType, resolutionsChain));
        }

        var instance = constructor.Invoke(parameters.ToArray());
        if (binding.Scope == Scope.Singleton)
        {
            binding.ImplementationObject = instance;
        }
        return instance;
    }
    
    public IEnumerable<object> ResolveAll(Type type)
    {
        var allTypes = _types.Values.SelectMany(list => list).Select(binding => binding.ImplementationType);
        var assignableTypes = allTypes.Where(type.IsAssignableFrom);

        foreach (var implType in assignableTypes)
        {
            // If you need to check for specific types like ViewInfo, Summarize, etc.,
            // you can perform additional checks here before yielding.
            yield return Resolve(implType);
        }
    }

    // Generic version for convenience
    public IEnumerable<T> ResolveAll<T>()
    {
        return ResolveAll(typeof(T)).Cast<T>();
    }



    class Binding
    {
        public Type ImplementationType { get; init; }
        
        
        public object? ImplementationObject { get; set; }
        
        public Scope Scope { get; init; }
    }
}