using FluentValidation;

namespace TaskPlusPlus.Application.DTOs.Category.Validators;
public class UpdateCategoryDtoValidator : AbstractValidator<UpdateCategoryDto>
{
    public UpdateCategoryDtoValidator()
    {
        Include(new InterfaceCategoryDtoValidator());
        RuleFor(dto => dto.Id).NotNull().WithMessage("{PropertyName} must be present");
    }
}
