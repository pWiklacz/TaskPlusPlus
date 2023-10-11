using FluentResults;
using TaskPlusPlus.Application.Contracts.Persistence;
using TaskPlusPlus.Application.DTOs.Category.Validators;
using TaskPlusPlus.Application.Messaging;
using TaskPlusPlus.Application.Responses.Errors;
using TaskPlusPlus.Application.Responses.Successes;
using TaskPlusPlus.Domain.Entities;
using TaskPlusPlus.Domain.Errors;
using TaskPlusPlus.Domain.ValueObjects.Category;

namespace TaskPlusPlus.Application.Features.Categories.Commands.EditCategory;
internal sealed class EditCategoryCommandHandler : ICommandHandler<EditCategoryCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public EditCategoryCommandHandler(IUnitOfWork unitOfWork)
    {
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

        var category = await _unitOfWork.Repository<Category, CategoryId>().GetByIdAsync(dto.Id);

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

        _unitOfWork.Repository<Category, CategoryId>().Update(category);

        var saveResult = await _unitOfWork.SaveChangesAsync(cancellationToken);

        if (saveResult <= 0)
        {
            return Result.Fail(new UpdatingProblemError(nameof(Category)));
        }

        return Result.Ok()
            .WithSuccess(new UpdateSuccess(nameof(Category))); ;
    }
}
