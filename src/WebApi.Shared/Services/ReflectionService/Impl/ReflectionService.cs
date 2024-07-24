using System.Reflection;

namespace WebApi.Shared.Services.ReflectionService.Impl;

public class ReflectionService : IReflectionService
{
    public IEnumerable<Type> GetClassTypes<T>(Assembly assembly)
    {
        return assembly
            .GetTypes()
            .Where(type => typeof(T).IsAssignableFrom(type) && type.IsClass && !type.IsAbstract);
    }

}