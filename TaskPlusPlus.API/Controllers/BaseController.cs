using FluentResults;
using Microsoft.AspNetCore.Mvc;

using TaskPlusPlus.Domain.Errors;

namespace TaskPlusPlus.API.Controllers;

public class BaseController : Controller
{
    protected new IActionResult Ok()
    {
        return base.Ok();
    }

    protected IActionResult Ok<T>(T result)
    {
        return base.Ok(result);
    }

    protected IActionResult FromResult(Result result)
    {
        if (result.IsSuccess)
            return Ok();
     
        var error = result.Errors.Find(e => e.GetType() == typeof(NotFoundError));

        if (error != null)
        {
            return NotFound(error);
        }

        return BadRequest(result.Errors);
    }
}