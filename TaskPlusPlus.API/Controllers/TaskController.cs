using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskPlusPlus.Application.DTOs.Common;
using TaskPlusPlus.Application.DTOs.Task;
using TaskPlusPlus.Application.Features.Tasks.Commands.ChangeTaskCategory;
using TaskPlusPlus.Application.Features.Tasks.Commands.ChangeTaskCompleteStatus;
using TaskPlusPlus.Application.Features.Tasks.Commands.ChangeTaskEnergy;
using TaskPlusPlus.Application.Features.Tasks.Commands.ChangeTaskPriority;
using TaskPlusPlus.Application.Features.Tasks.Commands.ChangeTaskProject;
using TaskPlusPlus.Application.Features.Tasks.Commands.CreateTask;
using TaskPlusPlus.Application.Features.Tasks.Commands.DeleteTask;
using TaskPlusPlus.Application.Features.Tasks.Commands.UpdateTaskDueDate;
using TaskPlusPlus.Application.Features.Tasks.Commands.UpdateTaskNameAndNotes;
using TaskPlusPlus.Application.Features.Tasks.Commands.UpdateTaskTags;
using TaskPlusPlus.Application.Features.Tasks.Queries.GetGroupedTasks;
using TaskPlusPlus.Application.Features.Tasks.Queries.GetTaskById;
using TaskPlusPlus.Application.Helpers;

namespace TaskPlusPlus.API.Controllers;

[Authorize]
public class TaskController : BaseController
{
    private readonly IMediator _mediator;

    public TaskController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<Dictionary<object, List<TaskDto>>>> Get(
        [FromQuery] TaskQueryParameters queryParameters)
    {
        var projects = await _mediator.Send(new GetGroupedTasksQuery(queryParameters));

        return FromResult(projects);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TaskDto>> Get(ulong id)
    {
        var query = new GetTaskByIdQuery(id);
        var task = await _mediator.Send(query);

        return FromResult(task);
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] CreateTaskDto dto)
    {
        var command = new CreateTaskCommand(dto);
        var result = await _mediator.Send(command);

        return FromResult(result);
    }

    [HttpPut("{id}/changeCompleteStatus")]
    public async Task<ActionResult> ChangeCompleteStatus([FromBody] ChangeCompleteStatusDto dto)
    {
        var command = new ChangeTaskCompleteStatusCommand(dto);
        var result = await _mediator.Send(command);

        return FromResult(result);
    }

    [HttpPut("{id}/updateNameAndNotes")]
    public async Task<ActionResult> UpdateNameAndNotes([FromBody] UpdateNameAndNotesDto dto)
    {
        var command = new UpdateTaskNameAndNotesCommand(dto);
        var result = await _mediator.Send(command);

        return FromResult(result);
    }

    [HttpPut("{id}/updateDueDate")]
    public async Task<ActionResult> UpdateDueDate([FromBody] UpdateDueDateDto dto)
    {
        var command = new UpdateTaskDueDateCommand(dto);
        var result = await _mediator.Send(command);

        return FromResult(result);
    }

    [HttpPut("{id}/updateCategory")]
    public async Task<ActionResult> UpdateCategory([FromBody] ChangeTaskCategoryDto dto)
    {
        var command = new ChangeTaskCategoryCommand(dto);
        var result = await _mediator.Send(command);

        return FromResult(result);
    }

    [HttpPut("{id}/updateEnergy")]
    public async Task<ActionResult> UpdateDueDate([FromBody] ChangeTaskEnergyDto dto)
    {
        var command = new ChangeTaskEnergyCommand(dto);
        var result = await _mediator.Send(command);

        return FromResult(result);
    }

    [HttpPut("{id}/updatePriority")]
    public async Task<ActionResult> UpdatePriority([FromBody] ChangeTaskPriorityDto dto)
    {
        var command = new ChangeTaskPriorityCommand(dto);
        var result = await _mediator.Send(command);

        return FromResult(result);
    }

    [HttpPut("{id}/updateProject")]
    public async Task<ActionResult> UpdateProject([FromBody] ChangeTaskProjectDto dto)
    {
        var command = new ChangeTaskProjectCommand(dto);
        var result = await _mediator.Send(command);

        return FromResult(result);
    }

    [HttpPut("{id}/updateTags")]
    public async Task<ActionResult> UpdateTags([FromBody] UpdateTaskTagsDto dto)
    {
        var command = new UpdateTaskTagsCommand(dto);
        var result = await _mediator.Send(command);

        return FromResult(result);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(ulong id)
    {
        var command = new DeleteTaskCommand(id);
        var result = await _mediator.Send(command);

        return FromResult(result);
    }
}