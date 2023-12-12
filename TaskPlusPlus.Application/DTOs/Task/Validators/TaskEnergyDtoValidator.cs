using FluentValidation;
using TaskPlusPlus.Application.DTOs.Task.IDto;
using TaskPlusPlus.Domain.Enums;

namespace TaskPlusPlus.Application.DTOs.Task.Validators;
internal class TaskEnergyDtoValidator : AbstractValidator<ITaskEnergyDto>
{
    public TaskEnergyDtoValidator()
    {
        RuleFor(dto => dto.Energy)
            .Must(e =>
            {
                var result = Energy.FromValue(e);
                return result.IsSuccess;
            }).WithMessage("There is no Energy value name {PropertyValue}");

    }
}
