using FluentResults;
using FluentValidation.Results;
using TaskPlusPlus.Domain.Errors;

namespace TaskPlusPlus.Application.Responses.Errors;

public class ValidationError : BaseError
{
    public ValidationError(ValidationResult validationResult, string name)
        : base(400, $"Validation failed for {name} object. Error count: {validationResult.Errors.Count}")
    {
        foreach (var error in validationResult.Errors)
        {
            Metadata.Add(error.PropertyName, error.ErrorMessage);
        }
    }
}