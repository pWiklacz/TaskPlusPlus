using FluentResults;
using TaskPlusPlus.Application.Contracts.Persistence;
using TaskPlusPlus.Application.Messaging;
using TaskPlusPlus.Application.Responses.Errors;
using TaskPlusPlus.Application.Responses.Successes;
using TaskPlusPlus.Domain.Entities;
using TaskPlusPlus.Domain.Errors;
using TaskPlusPlus.Domain.ValueObjects.Project;

namespace TaskPlusPlus.Application.Features.Projects.Commands.ChangeProjectCompleteStatus;
internal sealed class ChangeProjectCompleteStatusCommandHandler : ICommandHandler<ChangeProjectCompleteStatusCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public ChangeProjectCompleteStatusCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(ChangeProjectCompleteStatusCommand request, CancellationToken cancellationToken)
    {
        var project = await _unitOfWork.Repository<Project, ProjectId>().GetByIdAsync(request.Dto.Id);

        if (project is null)
        {
            return Result.Fail(new NotFoundError(nameof(Project), request.Dto.Id));
        }

        project.ChangeCompleteState(request.Dto.IsComplete);

        _unitOfWork.Repository<Project, ProjectId>().Update(project);

        var saveResult = await _unitOfWork.SaveChangesAsync(cancellationToken);

        if (saveResult <= 0)
        {
            return Result.Fail(new UpdatingProblemError(nameof(Project)));
        }

        return Result.Ok()
            .WithSuccess(new CompleteSuccess(nameof(Project)));
    }
}
