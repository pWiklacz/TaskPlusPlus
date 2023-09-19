using FluentValidation;

namespace TaskPlusPlus.Application.DTOs.Tag.Validators;

public class UpdateTagDtoValidator : AbstractValidator<UpdateTagDto>
{
    public UpdateTagDtoValidator()
    {
        Include(new InterfaceTagDtoValidator());
        RuleFor(dto => dto.Id).NotNull().WithMessage("{PropertyName} must be present");
    }
}