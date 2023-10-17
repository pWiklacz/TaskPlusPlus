using FluentValidation;
using TaskPlusPlus.Application.DTOs.Common.Validators;

namespace TaskPlusPlus.Application.DTOs.Project.Validators;
internal class InterfaceProjectDtoValidator : AbstractValidator<IProjectDto>
{
    public InterfaceProjectDtoValidator()
    {
       Include(new DueDateDtoValidator());
       Include(new NameAndNotesValidator());
    }
}
