using FluentResults;
using Microsoft.AspNetCore.Mvc;

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
        if (result.IsSuccess)
            return Ok(result.Value); //Think about it later. Idk is better to just return result or value from result.

        if (result.HasError<BaseError>(e => e.Code == 401, out var errors))
            return Unauthorized(errors);
        if (result.HasError(e => e.Code == 404, out errors))
            return NotFound(errors);
        if (result.HasError(e => e.Code == 409, out errors))
            return Conflict(errors);

        return BadRequest(result.Reasons);
    }

    protected ActionResult FromResult(Result result)
    {
        if (result.IsSuccess)
            return Ok(result); //Think about it later. Idk is better to just return result or value from result.

        if (result.HasError<BaseError>(e => e.Code == 401, out var errors))
            return Unauthorized(errors);
        if (result.HasError(e => e.Code == 404, out errors))
            return NotFound(errors);
        if (result.HasError(e => e.Code == 409, out errors))
            return Conflict(errors);

        return BadRequest(result);
    }
}