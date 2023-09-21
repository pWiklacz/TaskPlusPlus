using FluentValidation;

namespace TaskPlusPlus.Application.DTOs.Project.Validators;
public class InterfaceProjectDtoValidator : AbstractValidator<IProjectDto>
{
    private const short MaxNameLength = 500;
    private const short MaxNotesLength = 10000;
    public InterfaceProjectDtoValidator()
    {
        RuleFor(dto => dto.Name)
            .NotEmpty().WithMessage("{PropertyName} is required")
            .NotNull()
            .MaximumLength(MaxNameLength).WithMessage($"Tag name character limit {{PropertyValue}}/{MaxNameLength}");

        RuleFor(dto => dto.Notes)
            .NotNull()
            .MaximumLength(MaxNameLength).WithMessage($"Tag name character limit {{PropertyValue}}/{MaxNotesLength}");

        RuleFor(dto => dto.DueDate)
            .GreaterThan(DateTime.Now).WithMessage("Due date cannot be in the past.\"");
    }
}
