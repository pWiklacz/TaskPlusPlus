using FluentResults;
using TaskPlusPlus.Application.Responses.Errors;
using TaskPlusPlus.Domain.Errors;

namespace TaskPlusPlus.API.Errors;

public class ApiResponse<T>
{
    public ApiResponse(Result result)
    {
        if (result.IsSuccess)
        {
            StatusCode = 200;
            Message = result.Reasons.Any() ? result.Successes.First().Message : string.Empty;
        }
        else
        {
            if (result.HasError<BaseError>(out var errors))
            {
                var error = errors.First();
                if (error.Metadata.Any())
                {
                    Value = error.Metadata.Values;
                }
                Message = error.Message;
                StatusCode = error.Code;
            }
            else
            {
                Message = "Something went wrong";
                StatusCode = 400;
            }
        }
    }

    public ApiResponse(Result<T> result) : this(result.ToResult())
    {
        if (result.IsSuccess && result.Value != null)
            Value = result.Value;
    }

    public int StatusCode { get; set; }
    public string? Message { get; set; }
    public object? Value { get; set; }
}