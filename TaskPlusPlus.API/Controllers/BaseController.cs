using FluentResults;
using Microsoft.AspNetCore.Mvc;
using TaskPlusPlus.API.Errors;
using TaskPlusPlus.Domain.Entities;
using TaskPlusPlus.Domain.Errors;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TaskPlusPlus.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BaseController : ControllerBase
{
    protected new ActionResult Ok()
    {
        return base.Ok();
    }

    protected ActionResult Ok<T>(T result)
    {
        return base.Ok(result);
    }

    protected ActionResult FromResult<T>(Result<T> result)
    {
        if (!ModelState.IsValid)
        {
            var errors = (from state in ModelState from error in state.Value.Errors select error.ErrorMessage).ToList();
            return BadRequest(new ApiResponse<T>(Result.Fail(new ModelStateInvalidError(errors))));
        }

        if (result.IsSuccess)
            return Ok(new ApiResponse<T>(result));

        if (result.HasError<BaseError>(e => e.Code == 401, out _))
            return Unauthorized(new ApiResponse<T>(result));
        if (result.HasError(e => e.Code == 404, out IEnumerable<BaseError> _))
            return NotFound(new ApiResponse<T>(result));
        if (result.HasError(e => e.Code == 409, out IEnumerable<BaseError> _))
            return Conflict(new ApiResponse<T>(result));

        return BadRequest(new ApiResponse<T>(result));
    }

    protected ActionResult FromResult(Result result)
    {
        if (!ModelState.IsValid)
        {
            var errors = (from state in ModelState from error in state.Value.Errors select error.ErrorMessage).ToList();
            return BadRequest(new ApiResponse<object>(Result.Fail(new ModelStateInvalidError(errors))));
        }

        var apiResponse = new ApiResponse<object>(result); 

        if (result.IsSuccess)
            return Ok(apiResponse);

        if (result.HasError<BaseError>(e => e.Code == 401, out _))
            return Unauthorized(apiResponse);

        if (result.HasError(e => e.Code == 404, out IEnumerable<BaseError> _))
            return NotFound(apiResponse);

        if (result.HasError(e => e.Code == 409, out IEnumerable<BaseError> _))
            return Conflict(apiResponse);

        return BadRequest(apiResponse);
    }
}