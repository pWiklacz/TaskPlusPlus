using FluentResults;
using TaskPlusPlus.Application.Contracts.Persistence;
using TaskPlusPlus.Application.Contracts.Persistence.Repositories;
using TaskPlusPlus.Application.Messaging;
using TaskPlusPlus.Application.Responses.Successes;
using TaskPlusPlus.Domain.Errors;
using Task = TaskPlusPlus.Domain.Entities.Task;

namespace TaskPlusPlus.Application.Features.Tasks.Commands.DeleteTask;
internal sealed class DeleteTaskCommandHandler : ICommandHandler<DeleteTaskCommand>
{
    private readonly ITaskRepository _taskRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteTaskCommandHandler(ITaskRepository taskRepository, IUnitOfWork unitOfWork)
    {
        _taskRepository = taskRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
    {
        var task = await _taskRepository.GetByIdAsync(request.Id);

        if (task is null)
        {
            return Result.Fail(new NotFoundError(nameof(Task), request.Id));
        }

        _taskRepository.Remove(task);
        await _unitOfWork.SaveChangesAsync(cancellationToken);


        return Result.Ok()
            .WithSuccess(new DeleteSuccess(nameof(Task)));
    }
}
