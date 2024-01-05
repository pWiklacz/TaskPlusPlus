using FluentResults;
using TaskPlusPlus.Application.Contracts.Persistence;
using TaskPlusPlus.Application.DTOs.Task.Validators;
using TaskPlusPlus.Application.Messaging;
using TaskPlusPlus.Application.Responses.Errors;
using TaskPlusPlus.Application.Responses.Successes;
using TaskPlusPlus.Domain.Entities;
using TaskPlusPlus.Domain.Errors;
using TaskPlusPlus.Domain.ValueObjects.Tag;
using TaskPlusPlus.Domain.ValueObjects.Task;
using Task = TaskPlusPlus.Domain.Entities.Task;

namespace TaskPlusPlus.Application.Features.Tasks.Commands.UpdateTaskTags;
internal sealed class UpdateTaskTagsCommandHandler : ICommandHandler<UpdateTaskTagsCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateTaskTagsCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<Result> Handle(UpdateTaskTagsCommand request, CancellationToken cancellationToken)
    {
        var validator = new TaskTagsDtoValidator(_unitOfWork);
        var validationResult = await validator.ValidateAsync(request.Dto, cancellationToken);

        if (validationResult.IsValid is false)
        {
            return Result.Fail(new ValidationError(validationResult, nameof(Task)));
        }

        var task = await _unitOfWork.Repository<Task, TaskId>().GetByIdAsync(request.Dto.Id);

        if (task == null)
        {
            return Result.Fail(new NotFoundError(nameof(Task), request.Dto.Id));
        }

        var tagsIds = request.Dto.Tags;
        var tags = new List<Tag>();

        foreach (var tagId in tagsIds)
        {
            var tag = await _unitOfWork.Repository<Tag, TagId>().GetByIdAsync(tagId);
            tags.Add(tag!);
        }

        var updateTagsResult = task.UpdateTags(tags);
        if (updateTagsResult.IsFailed)
            return updateTagsResult;

        _unitOfWork.Repository<Task, TaskId>().Update(task);
        var saveResult = await _unitOfWork.SaveChangesAsync(cancellationToken);

        if (saveResult <= 0)
        {
            return Result.Fail(new UpdatingProblemError(nameof(Task)));
        }

        return Result.Ok()
            .WithSuccess(new UpdateSuccess(nameof(Task)));

    }
}
