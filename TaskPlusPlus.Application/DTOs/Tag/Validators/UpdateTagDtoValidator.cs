using FluentValidation;

namespace TaskPlusPlus.Application.DTOs.Tag.Validators;

public class UpdateTagDtoValidator : AbstractValidator<UpdateTagDto>
{
    public UpdateTagDtoValidator()
    {
        Include(new InterfaceTagDtoValidator());
        RuleFor(p => p.Id).NotNull().WithMessage("{PropertyName} must be present");
    }
}