using FluentValidation;
using TaskPlusPlus.Application.Contracts.Persistence;
using TaskPlusPlus.Domain.ValueObjects.Category;

namespace TaskPlusPlus.Application.DTOs.Task.Validators;
internal class ChangeTaskCategoryDtoValidator : AbstractValidator<ChangeTaskCategoryDto>
{
    public ChangeTaskCategoryDtoValidator(IUnitOfWork unitOfWork)
    {

        RuleFor(dto => dto.CategoryId)
            .Cascade(CascadeMode.Stop)
        .NotNull()
        .GreaterThan(0UL)
        .MustAsync(async (id, token) =>
        {
            var categoryExists = await unitOfWork.Repository<Domain.Entities.Category, CategoryId>()
                .ExistsByIdAsync(id);
            return categoryExists;
        })
            .WithMessage("{PropertyName} does not exist.");
    }
}
