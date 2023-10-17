using FluentResults;
using TaskPlusPlus.Application.Contracts.Persistence;
using TaskPlusPlus.Application.Messaging;
using TaskPlusPlus.Application.Responses.Errors;
using TaskPlusPlus.Application.Responses.Successes;
using TaskPlusPlus.Domain.Entities;
using TaskPlusPlus.Domain.Errors;
using TaskPlusPlus.Domain.ValueObjects.Project;

namespace TaskPlusPlus.Application.Features.Projects.Commands.DeleteProject;
internal class DeleteProjectCommandHandler : ICommandHandler<DeleteProjectCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteProjectCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<Result> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await _unitOfWork.Repository<Project, ProjectId>().GetByIdAsync(request.Id);

        if (project is null)
        {
            return Result.Fail(new NotFoundError(nameof(Project), request.Id));
        }

        _unitOfWork.Repository<Project, ProjectId>().Remove(project);

        var saveResult = await _unitOfWork.SaveChangesAsync(cancellationToken);

        if (saveResult <= 0)
        {
            return Result.Fail(new DeletingProblemError(nameof(Project)));
        }
        return Result.Ok()
            .WithSuccess(new DeleteSuccess(nameof(Project)));
    }
}
