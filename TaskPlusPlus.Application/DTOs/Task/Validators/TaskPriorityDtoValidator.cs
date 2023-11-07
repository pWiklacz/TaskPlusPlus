using FluentValidation;
using TaskPlusPlus.Application.DTOs.Task.IDto;
using TaskPlusPlus.Domain.Enums;

namespace TaskPlusPlus.Application.DTOs.Task.Validators;
internal class TaskPriorityDtoValidator : AbstractValidator<ITaskPriorityDto>
{
    public TaskPriorityDtoValidator()
    {
        RuleFor(dto => dto.Priority)
            .Must(e =>
                {
                    var result = Priority.FromName(e);
                    return result.IsSuccess;
                }
                ).WithMessage("There is no Priority value name {PropertyValue}");
    }
}
