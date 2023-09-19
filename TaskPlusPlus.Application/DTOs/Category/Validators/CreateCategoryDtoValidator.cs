using FluentValidation;

namespace TaskPlusPlus.Application.DTOs.Category.Validators;
public class CreateCategoryDtoValidator : AbstractValidator<CreateCategoryDto>
{
    public CreateCategoryDtoValidator()
    {
        Include(new InterfaceCategoryDtoValidator());
    }
}
