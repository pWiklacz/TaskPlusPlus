using FluentValidation;
using TaskPlusPlus.Application.DTOs.Common.Validators;

namespace TaskPlusPlus.Application.DTOs.Project.Validators;
public class EditProjectDtoValidator : AbstractValidator<EditProjectDto>
{
    public EditProjectDtoValidator(bool dateChanged = false)
    {
        if (dateChanged)
        {
            Include(new DueDateDtoValidator());
        }
        Include(new NameAndNotesValidator());
    }
}
