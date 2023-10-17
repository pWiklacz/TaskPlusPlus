using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskPlusPlus.Application.DTOs.Tag;
using TaskPlusPlus.Application.Features.Tags.Commands.CreateTag;
using TaskPlusPlus.Application.Features.Tags.Commands.DeleteTag;
using TaskPlusPlus.Application.Features.Tags.Commands.EditTag;
using TaskPlusPlus.Application.Features.Tags.Queries.GetTagById;
using TaskPlusPlus.Application.Features.Tags.Queries.GetTags;

namespace TaskPlusPlus.API.Controllers;

[Authorize]
public class TagController : BaseController
{
    private readonly IMediator _mediator;

    public TagController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<TagDto>>> Get()
    {
        var query = new GetTagsQuery();
        var tags = await _mediator.Send(query);

        return FromResult(tags);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TagDto>> Get(ulong id)
    {
        var query = new GetTagByIdQuery(id);
        var tag = await _mediator.Send(query);

        return FromResult(tag);
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] CreateTagDto dto)
    {
        var command = new CreateTagCommand(dto);
        var result = await _mediator.Send(command);

        return FromResult(result);
    }

    [HttpPut]
    public async Task<ActionResult> Put([FromBody] UpdateTagDto dto)
    {
        var command = new EditTagCommand(dto);
        var result = await _mediator.Send(command);

        return FromResult(result);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(ulong id)
    {
        var command = new DeleteTagCommand(id);
        var result = await _mediator.Send(command);

        return FromResult(result);
    }
}
