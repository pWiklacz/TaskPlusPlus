using FluentValidation;
using TaskPlusPlus.Application.DTOs.Common.IDto;

namespace TaskPlusPlus.Application.DTOs.Common.Validators;
internal class NameAndNotesValidator : AbstractValidator<INameAndNotesDto>
{
    private const short MaxNameLength = 500;
    private const short MaxNotesLength = 10000;
    public NameAndNotesValidator()
    {
        RuleFor(dto => dto.Name)
            .NotEmpty().WithMessage("{PropertyName} is required")
            .NotNull()
            .MaximumLength(MaxNameLength).WithMessage($"Tag name character limit {{PropertyValue}}/{MaxNameLength}");

        RuleFor(dto => dto.Notes)
            .NotNull()
            .MaximumLength(MaxNameLength).WithMessage($"Tag name character limit {{PropertyValue}}/{MaxNotesLength}");
    }
}
