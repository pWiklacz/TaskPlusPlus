using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskPlusPlus.Application.DTOs.Category;
using TaskPlusPlus.Application.Features.Categories.Commands.CreateCategory;
using TaskPlusPlus.Application.Features.Categories.Commands.DeleteCategory;
using TaskPlusPlus.Application.Features.Categories.Commands.EditCategory;
using TaskPlusPlus.Application.Features.Categories.Queries.GetCategoryById;
using TaskPlusPlus.Application.Features.Categories.Queries.GetUserCategories;

namespace TaskPlusPlus.API.Controllers;

[Authorize]
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
        var categories = await _mediator.Send(new GetUserCategoriesQuery());

        return FromResult(categories);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CategoryDto>> Get(ulong id)
    {
        var query = new GetCategoryByIdQuery(id);
        var category = await _mediator.Send(query);

        return FromResult(category);
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] CreateCategoryDto dto)
    {
        var command = new CreateCategoryCommand(dto);
        var result = await _mediator.Send(command);

        return FromResult(result); 
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Put([FromBody] UpdateCategoryDto dto, ulong id)
    {
        dto.Id = id;
        var command = new EditCategoryCommand(dto);
        var result = await _mediator.Send(command);

        return FromResult(result);
    }


    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(ulong id)
    {
        var command = new DeleteCategoryCommand(id);
        var result = await _mediator.Send(command);

        return FromResult(result);
    }
}
