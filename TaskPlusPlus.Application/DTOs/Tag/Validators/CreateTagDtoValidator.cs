using FluentValidation;

namespace TaskPlusPlus.Application.DTOs.Tag.Validators;

public class CreateTagDtoValidator : AbstractValidator<CreateTagDto>
{
    public CreateTagDtoValidator()
    {
        Include(new InterfaceTagDtoValidator());
    }
}