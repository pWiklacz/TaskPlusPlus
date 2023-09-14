using FluentResults;
using FluentValidation.Results;

namespace TaskPlusPlus.Application.Responses.Errors;

internal class ValidationError : Error
{
    public ValidationError(ValidationResult validationResult, string name)
        : base($"Validation failed for {name} object. Error count: {validationResult.Errors.Count}")
    {
        foreach (var error in validationResult.Errors)
        {
            Metadata.Add(error.PropertyName, error.ErrorMessage);
        }
    }
}