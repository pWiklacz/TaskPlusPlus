using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskPlusPlus.Application.DTOs.Common;
using TaskPlusPlus.Application.DTOs.Project;
using TaskPlusPlus.Application.Features.Projects.Commands.ChangeProjectCompleteStatus;
using TaskPlusPlus.Application.Features.Projects.Commands.CreateProject;
using TaskPlusPlus.Application.Features.Projects.Commands.DeleteProject;
using TaskPlusPlus.Application.Features.Projects.Commands.EditProject;
using TaskPlusPlus.Application.Features.Projects.Commands.UpdateProjectDueDate;
using TaskPlusPlus.Application.Features.Projects.Commands.UpdateProjectNameAndNotes;
using TaskPlusPlus.Application.Features.Projects.Queries.GetProjectById;
using TaskPlusPlus.Application.Features.Projects.Queries.GetUserProjects;

namespace TaskPlusPlus.API.Controllers;

[Authorize]
public class ProjectController : BaseController
{
    private readonly IMediator _mediator;

    public ProjectController(IMediator mediator)
    {
        _mediator = mediator;
    }  

    [HttpGet]
    public async Task<ActionResult<List<ProjectDto>>> Get()
    {
        var projects = await _mediator.Send(new GetUserProjectsQuery());

        return FromResult(projects);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProjectDto>> Get(ulong id)
    {
        var query = new GetProjectByIdQuery(id);
        var project = await _mediator.Send(query);

        return FromResult(project);
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] CreateProjectDto dto)
    {
        var command = new CreateProjectCommand(dto);
        var result = await _mediator.Send(command);

        return FromResult(result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Put([FromBody] EditProjectDto dto)
    {
        var command = new EditProjectCommand(dto);
        var result = await _mediator.Send(command);

        return FromResult(result);
    }

    [HttpPut("{id}/changeCompleteStatus")]
    public async Task<ActionResult> ChangeCompleteStatus([FromBody] ChangeCompleteStatusDto dto)
    {
        var command = new ChangeProjectCompleteStatusCommand(dto);
        var result = await _mediator.Send(command);

        return FromResult(result);
    }

    [HttpPut("{id}/updateNameAndNotes")]
    public async Task<ActionResult> UpdateNameAndNotes([FromBody] UpdateNameAndNotesDto dto)
    {
        var command = new UpdateProjectNameAndNotesCommand(dto);
        var result = await _mediator.Send(command);

        return FromResult(result);
    }

    [HttpPut("{id}/updateDueDate")]
    public async Task<ActionResult> UpdateDueDate([FromBody] UpdateDueDateDto dto)
    {
        var command = new UpdateProjectDueDateCommand(dto);
        var result = await _mediator.Send(command);

        return FromResult(result);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(ulong id)
    {
        var command = new DeleteProjectCommand(id);
        var result = await _mediator.Send(command);

        return FromResult(result);
    }
}
