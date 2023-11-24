using FluentValidation;

namespace TaskPlusPlus.Application.DTOs.Category.Validators;

public class InterfaceCategoryDtoValidator : AbstractValidator<ICategoryDto>
{
    private const string HexPattern = @"^#([A-Fa-f0-9]{6}|[A-Fa-f0-9]{3})$";
    private const byte MaxNameLength = 50;

    public InterfaceCategoryDtoValidator()
    {
        RuleFor(dto => dto.Icon).NotEmpty()
            .WithMessage("{PropertyName} is required");

        RuleFor(dto => dto.ColorHex)
            .NotEmpty().WithMessage("{PropertyName} is required")
            .NotNull()
            .Matches(HexPattern);

        RuleFor(dto => dto.Name)
            .NotEmpty().WithMessage("{PropertyName} is required")
            .NotNull()
            .MaximumLength(MaxNameLength).WithMessage($"Category name character limit {{PropertyValue}}/{MaxNameLength}");
    }
}