using Microsoft.AspNetCore.Identity;
using TaskPlusPlus.Domain.Errors;

namespace TaskPlusPlus.API.Errors;

public class ModelStateInvalidError : BaseError
{
    public ModelStateInvalidError(List<string> errors)
        : base(400, $"Model state invalid. Error count: {errors!.Count()}")
    {
        foreach (var error in errors.Select((message, index) => (message, index)))
        {
            Metadata.Add(error.index.ToString(), error.message);
        }
    }
}
