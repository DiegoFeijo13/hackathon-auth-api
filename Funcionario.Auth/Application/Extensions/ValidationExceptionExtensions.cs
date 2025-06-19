using FluentValidation;

namespace Application.Extensions;

public static class ValidationExceptionExtensions
{
    public static string[] ToResultMessage(this ValidationException exception) 
    {
        if (exception.Errors == null || !exception.Errors.Any())
            return [];
        
        return exception.Errors.Select(x => x.ErrorMessage).ToArray();
    }
}