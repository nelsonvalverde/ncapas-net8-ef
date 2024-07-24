using System.Reflection;

namespace WebApi.Shared.Services.ReflectionService;

/// <summary>
/// Get Reflection from assembly
/// </summary>
public interface IReflectionService
{
    IEnumerable<Type> GetClassTypes<T>(Assembly assembly);
}