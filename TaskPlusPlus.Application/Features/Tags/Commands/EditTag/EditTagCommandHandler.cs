using FluentResults;
using TaskPlusPlus.Application.Contracts.Persistence.Repositories;
using TaskPlusPlus.Application.Contracts.Persistence;
using TaskPlusPlus.Application.DTOs.Tag.Validators;
using TaskPlusPlus.Application.Messaging;
using TaskPlusPlus.Application.Responses.Errors;
using TaskPlusPlus.Application.Responses.Successes;
using TaskPlusPlus.Domain.Entities;
using Task = TaskPlusPlus.Domain.Entities.Task;
using TaskPlusPlus.Domain.Errors;

namespace TaskPlusPlus.Application.Features.Tags.Commands.EditTag;

internal sealed class EditTagCommandHandler : ICommandHandler<EditTagCommand>
{
    private readonly ITagRepository _tagRepository;
    private readonly IUnitOfWork _unitOfWork;

    public EditTagCommandHandler(ITagRepository tagRepository, IUnitOfWork unitOfWork)
    {
        _tagRepository = tagRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<Result> Handle(EditTagCommand request, CancellationToken cancellationToken)
    {
        var dto = request.Dto;
        var validator = new UpdateTagDtoValidator();
        var validationResult = await validator.ValidateAsync(dto, cancellationToken);

        if (validationResult.IsValid is false)
        {
            return Result.Fail(new ValidationError(validationResult, nameof(Tag)));
        }

        var tag = await _tagRepository.GetByIdAsync(dto.Id);

        if (tag is null)
        {
            return Result.Fail(new NotFoundError(nameof(Tag), dto.Id));
        }

        var errors = new List<IError>();

        tag.ChangeFavoriteState(dto.IsFavorite);

        var updateNameResult = tag.UpdateName(dto.Name);
        if (updateNameResult.IsFailed)
            errors.AddRange(updateNameResult.Errors);
        var changeColorResult = tag.ChangeColor(dto.ColorHex);
        if(changeColorResult.IsFailed)
            errors.AddRange(changeColorResult.Errors);

        if (errors.Any())
            return Result.Fail(errors);

        _tagRepository.Update(tag);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Ok()
            .WithSuccess(new UpdateSuccess(nameof(Tag))); ;
    }
}  