using FluentValidation;
using TaskPlusPlus.Application.DTOs.Task.IDto;
using TaskPlusPlus.Domain.Enums;

namespace TaskPlusPlus.Application.DTOs.Task.Validators;
internal class TaskPriorityDtoValidator : AbstractValidator<ITaskPriorityDto>
{
    public TaskPriorityDtoValidator()
    {
        RuleFor(dto => dto.Priority)
            .NotNull()
            .Must(e => Priority.FromName(e) != null).WithMessage("There is no Priority value name {PropertyName}");
    }
}
