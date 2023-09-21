﻿using FluentResults;
using TaskPlusPlus.Application.Contracts.Persistence;
using TaskPlusPlus.Application.Contracts.Persistence.Repositories;
using TaskPlusPlus.Application.DTOs.Project.Validators;
using TaskPlusPlus.Application.Messaging;
using TaskPlusPlus.Application.Models.Identity.ApplicationUser;
using TaskPlusPlus.Application.Responses.Errors;
using TaskPlusPlus.Application.Responses.Successes;
using TaskPlusPlus.Domain.Entities;

namespace TaskPlusPlus.Application.Features.Projects.Commands.CreateProject;
internal sealed class CreateProjectCommandHandler : ICommandHandler<CreateProjectCommand>
{
    private readonly IProjectRepository _projectRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserContext _userContext;

    public CreateProjectCommandHandler(IProjectRepository projectRepository, IUnitOfWork unitOfWork, IUserContext userContext)
    {
        _projectRepository = projectRepository;
        _unitOfWork = unitOfWork;
        _userContext = userContext;
    }

    public async Task<Result> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        var userResult = _userContext.GetCurrentUser();
        if (userResult.IsFailed)
        {
            return userResult.ToResult();
        }

        var userId = userResult.Value.Id;

        var dto = request.Dto;
        var validator = new CreateProjectDtoValidator();

        var validationResult = await validator.ValidateAsync(dto, cancellationToken);

        if (validationResult.IsValid is false)
        {
            return Result.Fail(new ValidationError(validationResult, nameof(Project)));
        }

        //TODO:: ProjectCategoryId
        var result = Project.Create(dto.Name, dto.Notes, dto.DueDate, userId, 1);

        if (result.IsFailed)
            return result.ToResult();

        var project = result.Value;

        _projectRepository.Add(project);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Ok()
            .WithSuccess(new CreationSuccess(nameof(Project)));
    }
}
