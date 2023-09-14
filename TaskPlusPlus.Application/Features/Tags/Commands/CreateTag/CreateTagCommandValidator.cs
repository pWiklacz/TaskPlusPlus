using FluentValidation;
using TaskPlusPlus.Application.DTOs.Tag;

namespace TaskPlusPlus.Application.Features.Tags.Commands.CreateTag;

internal class CreateTagCommandValidator : AbstractValidator<CreateTagDto>
{
    private const string HexPattern = @"^#([A-Fa-f0-9]{6}|[A-Fa-f0-9]{3})$";
    private const byte MaxNameLength = 50;
    public CreateTagCommandValidator()
    {
        RuleFor(dto => dto.ColorHex)
            .NotEmpty().WithMessage("{PropertyName} is required")
            .NotNull()
            .Matches(HexPattern);

        RuleFor(dto => dto.Name)
            .NotEmpty().WithMessage("{PropertyName} is required")
            .NotNull()
            .MaximumLength(MaxNameLength).WithMessage($"Tag name character limit {{PropertyValue}}/{MaxNameLength}");
    }
}