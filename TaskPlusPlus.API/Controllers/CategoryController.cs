using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskPlusPlus.Application.DTOs.Category;
using TaskPlusPlus.Application.Features.Categories.Queries.GetCategories;

namespace TaskPlusPlus.API.Controllers;

public class CategoryController : BaseController
{
    private readonly IMediator _mediator;

    public CategoryController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<CategoryDto>>> Get()
    {
        var categories = await _mediator.Send(new GetCategoriesQuery());
        return FromResult(categories);
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<List<CategoryDto>>> Get(ulong id)
    {

    }
}
