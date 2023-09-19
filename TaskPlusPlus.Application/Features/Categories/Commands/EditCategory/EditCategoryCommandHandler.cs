using FluentResults;
using TaskPlusPlus.Application.Contracts.Persistence.Repositories;
using TaskPlusPlus.Application.Contracts.Persistence;
using TaskPlusPlus.Application.DTOs.Category.Validators;
using TaskPlusPlus.Application.Messaging;
using TaskPlusPlus.Application.DTOs.Tag.Validators;
using TaskPlusPlus.Application.Responses.Errors;
using TaskPlusPlus.Domain.Entities;
using TaskPlusPlus.Domain.Errors;
using TaskPlusPlus.Application.Responses.Successes;

namespace TaskPlusPlus.Application.Features.Categories.Commands.EditCategory;
internal sealed class EditCategoryCommandHandler : ICommandHandler<EditCategoryCommand>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public EditCategoryCommandHandler(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
    {
        _categoryRepository = categoryRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<Result> Handle(EditCategoryCommand request, CancellationToken cancellationToken)
    {
        var dto = request.Dto;
        var validator = new UpdateCategoryDtoValidator();
        var validationResult = await validator.ValidateAsync(dto, cancellationToken);

        if (validationResult.IsValid is false)
        {
            return Result.Fail(new ValidationError(validationResult, nameof(Category)));
        }

        var category = await _categoryRepository.GetByIdAsync(dto.Id);

        if (category is null)
        {
            return Result.Fail(new NotFoundError(nameof(Category), dto.Id));
        }

        var errors = new List<IError>();

        category.ChangeFavoriteState(dto.IsFavorite);

        var updateNameResult = category.UpdateName(dto.Name);
        if (updateNameResult.IsFailed)
            errors.AddRange(updateNameResult.Errors);
        var changeColorResult = category.ChangeColor(dto.ColorHex);
        if (changeColorResult.IsFailed)
            errors.AddRange(changeColorResult.Errors);

        if (errors.Any())
            return Result.Fail(errors);

        _categoryRepository.Update(category);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Ok()
            .WithSuccess(new UpdateSuccess(nameof(Category))); ;
    }
}
