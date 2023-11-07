using FluentResults;

namespace TaskPlusPlus.API.Errors;

public class ApiValidationErrorResponse<T>
{
    public ApiValidationErrorResponse(Result result)
    {

    }


    public int StatusCode { get; set; }
    public string? Message { get; set; }

}