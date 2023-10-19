using FluentValidation;
using TaskPlusPlus.Application.DTOs.Task.IDto;
using TaskPlusPlus.Domain.Enums;

namespace TaskPlusPlus.Application.DTOs.Task.Validators;
internal class TaskEnergyDtoValidator : AbstractValidator<ITaskEnergyDto>
{
    public TaskEnergyDtoValidator()
    {
        RuleFor(dto => dto.Energy)
            .NotNull()
            .Must(e => Energy.FromName(e) != null).WithMessage("There is no Energy value name {PropertyName}");

    }
}
