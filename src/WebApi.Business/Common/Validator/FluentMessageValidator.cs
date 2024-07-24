namespace WebApi.Business.Common.Validator;

public static class FluentMessageValidator
{
    public static string NotEmpty => "Please specify a {PropertyName}";
    public static string Valid => "Please specify a valid {PropertyName}";
    public static string NotNull => "The {PropertyName} cannot be null";
    public static string NotBeZero => "The {PropertyName} cannot be 0";
    public static string ValidateEmail => "The {PropertyName} is not valid";
    public static string MaxLength => "The {PropertyName} must be 250 chars or fewer";
    public static string MinLength => "The {PropertyName} must be 10 chars or more";
}
