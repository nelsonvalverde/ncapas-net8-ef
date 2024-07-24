using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WebApi.Business.Common.Error;

public sealed class ValidationRequestException : Exception
{
    public ValidationRequestException() : base("One or more validation failures have occurred.")
    {
        Errors = new Dictionary<string, IEnumerable<string>>();
    }

    public ValidationRequestException(ModelStateDictionary modelState)
       : this()
    {
        var errors = new Dictionary<string, IEnumerable<string>>();
        foreach (var item in modelState)
        {
            if (item.Value.Errors.Count > 0)
            {
                errors.Add(item.Key, item.Value.Errors.Select(x => x.ErrorMessage));
            }
        }
        Errors = errors;
    }

    public IDictionary<string, IEnumerable<string>> Errors { get; }
}