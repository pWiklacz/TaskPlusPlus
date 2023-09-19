using FluentResults;
using TaskPlusPlus.Application.Contracts.Persistence;
using TaskPlusPlus.Application.Contracts.Persistence.Repositories;
using TaskPlusPlus.Application.DTOs.Tag.Validators;
using TaskPlusPlus.Application.Messaging;
using TaskPlusPlus.Application.Responses.Errors;
using TaskPlusPlus.Application.Responses.Successes;
using TaskPlusPlus.Domain.Entities;

namespace TaskPlusPlus.Application.Features.Tags.Commands.CreateTag;

internal sealed class CreateTagCommandHandler : ICommandHandler<CreateTagCommand>
{
    private readonly ITagRepository _tagRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateTagCommandHandler(ITagRepository tagRepository, IUnitOfWork unitOfWork)
    {
        _tagRepository = tagRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(CreateTagCommand request, CancellationToken cancellationToken)
    {
        var dto = request.Dto;
        var validator = new CreateTagDtoValidator();
        var validationResult = await validator.ValidateAsync(dto, cancellationToken);


        if (validationResult.IsValid is false)
        {
            return Result.Fail(new ValidationError(validationResult,nameof(Tag)));
        }
        
        //TODO:: UserID
        var result = Tag.Create(dto.Name, dto.ColorHex, "UserId", dto.IsFavorite);

        if (result.IsFailed)
            return result.ToResult();

        var tag = result.Value;

        _tagRepository.Add(tag);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return Result.Ok()
            .WithSuccess(new CreationSuccess(nameof(Tag)));
    }
}