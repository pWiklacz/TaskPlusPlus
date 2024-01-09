using FluentResults;
using TaskPlusPlus.Application.Contracts.Persistence;
using TaskPlusPlus.Application.Messaging;
using TaskPlusPlus.Application.Models.Identity.ApplicationUser;
using TaskPlusPlus.Application.Responses.Errors;
using TaskPlusPlus.Application.Responses.Successes;
using TaskPlusPlus.Application.Specifications.Project;
using TaskPlusPlus.Domain.Entities;
using TaskPlusPlus.Domain.Errors;
using TaskPlusPlus.Domain.ValueObjects.Project;

namespace TaskPlusPlus.Application.Features.Projects.Commands.ChangeProjectCompleteStatus;
internal sealed class ChangeProjectCompleteStatusCommandHandler : ICommandHandler<ChangeProjectCompleteStatusCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserContext _userContext;

    public ChangeProjectCompleteStatusCommandHandler(IUnitOfWork unitOfWork, IUserContext userContext)
    {
        _unitOfWork = unitOfWork;
        _userContext = userContext;
    }

    public async Task<Result> Handle(ChangeProjectCompleteStatusCommand request, CancellationToken cancellationToken)
    {
        var userResult = _userContext.GetCurrentUser();
        if (userResult.IsFailed)
        {
            return userResult.ToResult();
        }
        var dto = request.Dto;
        var userId = userResult.Value.Id;

        var spec = new UserProjectsWithTasksSpecification(dto.Id, userId);

        var project = await _unitOfWork.Repository<Project, ProjectId>().GetEntityWithSpec(spec);

        if (project is null)
        {
            return Result.Fail(new NotFoundError(nameof(Project), request.Dto.Id));
        }

        project.ChangeCompleteState(request.Dto.IsComplete);

        var completedTasksNumber = 0;

        foreach (var projectTask in project.Tasks)
        {
            if (projectTask.IsCompleted) continue;
            completedTasksNumber++;
            projectTask.ChangeCompleteState(true);
        }

        _unitOfWork.Repository<Project, ProjectId>().Update(project);

        var saveResult = await _unitOfWork.SaveChangesAsync(cancellationToken);

        if (saveResult <= 0)
        {
            return Result.Fail(new UpdatingProblemError(nameof(Project)));
        }

        return completedTasksNumber != 0
           ? Result.Ok().WithSuccess($"The project was successfully completed, including {completedTasksNumber} tasks.")
           : Result.Ok()
               .WithSuccess(new CompleteSuccess(nameof(Project)));
    }
}
