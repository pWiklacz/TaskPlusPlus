using FluentResults;
using TaskPlusPlus.Application.Contracts.Persistence;
using TaskPlusPlus.Application.Messaging;
using TaskPlusPlus.Application.Responses.Errors;
using TaskPlusPlus.Application.Responses.Successes;
using TaskPlusPlus.Domain.Errors;
using TaskPlusPlus.Domain.ValueObjects.Task;
using Task = TaskPlusPlus.Domain.Entities.Task;

namespace TaskPlusPlus.Application.Features.Tasks.Commands.DeleteTask;
internal sealed class DeleteTaskCommandHandler : ICommandHandler<DeleteTaskCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteTaskCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
    {
        var task = await _unitOfWork.Repository<Task, TaskId>().GetByIdAsync(request.Id);

        if (task is null)
        {
            return Result.Fail(new NotFoundError(nameof(Task), request.Id));
        }

        _unitOfWork.Repository<Task, TaskId>().Remove(task);
        var result = await _unitOfWork.SaveChangesAsync(cancellationToken);

        if (result <= 0)
        {
            return Result.Fail(new DeletingProblemError(nameof(Task)));
        }

        return Result.Ok()
            .WithSuccess(new DeleteSuccess(nameof(Task)));
    }
}
