using FluentResults;
using Microsoft.AspNetCore.Http;
using TaskPlusPlus.Application.Contracts.Persistence;
using TaskPlusPlus.Application.Contracts.Persistence.Repositories;
using TaskPlusPlus.Application.DTOs.Category.Validators;
using TaskPlusPlus.Application.Messaging;
using TaskPlusPlus.Application.Models.Identity.ApplicationUser;
using TaskPlusPlus.Application.Responses.Errors;
using TaskPlusPlus.Application.Responses.Successes;
using TaskPlusPlus.Domain.Entities;

namespace TaskPlusPlus.Application.Features.Categories.Commands.CreateCategory;
internal sealed class CreateCategoryCommandHandler : ICommandHandler<CreateCategoryCommand>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserContext _userContext;
 

    public CreateCategoryCommandHandler(
        ICategoryRepository categoryRepository, IUnitOfWork unitOfWork, IUserContext userContext)
    {
        _categoryRepository = categoryRepository;
        _unitOfWork = unitOfWork;
        _userContext = userContext;
    }

    public async Task<Result> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var userResult = _userContext.GetCurrentUser();
        if (userResult.IsFailed)
        {
            return userResult.ToResult();
        }

        var userId = userResult.Value.Id;

        var dto = request.Dto;
        var validator = new CreateCategoryDtoValidator();
        var validationResult = await validator.ValidateAsync(dto, cancellationToken);

        if (validationResult.IsValid is false)
        {
            return Result.Fail(new ValidationError(validationResult, nameof(Category)));
        }

        var result = Category.Create(dto.Name, isImmutable: false, dto.IsFavorite, dto.ColorHex, userId);

        if (result.IsFailed)
            return result.ToResult();

        var category = result.Value;
        _categoryRepository.Add(category);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Ok()
            .WithSuccess(new CreationSuccess(nameof(Category)));
    }
}
