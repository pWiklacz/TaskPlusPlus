﻿using FluentResults;
using TaskPlusPlus.Application.Contracts.Persistence;
using TaskPlusPlus.Application.Contracts.Persistence.Repositories;
using TaskPlusPlus.Application.DTOs.Tag.Validators;
using TaskPlusPlus.Application.Messaging;
using TaskPlusPlus.Application.Models.Identity.ApplicationUser;
using TaskPlusPlus.Application.Responses.Errors;
using TaskPlusPlus.Application.Responses.Successes;
using TaskPlusPlus.Domain.Entities;

namespace TaskPlusPlus.Application.Features.Tags.Commands.CreateTag;

internal sealed class CreateTagCommandHandler : ICommandHandler<CreateTagCommand>
{
    private readonly ITagRepository _tagRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserContext _userContext;
    public CreateTagCommandHandler(ITagRepository tagRepository, IUnitOfWork unitOfWork, IUserContext userContext)
    {
        _tagRepository = tagRepository;
        _unitOfWork = unitOfWork;
        _userContext = userContext;
    }

    public async Task<Result> Handle(CreateTagCommand request, CancellationToken cancellationToken)
    {
        var userResult = _userContext.GetCurrentUser();
        if (userResult.IsFailed)
        {
            return userResult.ToResult();
        }

        var userId = userResult.Value.Id;

        var dto = request.Dto;
        var validator = new CreateTagDtoValidator();
        var validationResult = await validator.ValidateAsync(dto, cancellationToken);


        if (validationResult.IsValid is false)
        {
            return Result.Fail(new ValidationError(validationResult,nameof(Tag)));
        }
        
        var result = Tag.Create(dto.Name, dto.ColorHex, userId, dto.IsFavorite);

        if (result.IsFailed)
            return result.ToResult();

        var tag = result.Value;

        _tagRepository.Add(tag);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return Result.Ok()
            .WithSuccess(new CreationSuccess(nameof(Tag)));
    }
}