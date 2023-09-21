using FluentValidation;

namespace TaskPlusPlus.Application.DTOs.Project.Validators;
public class CreateProjectDtoValidator : AbstractValidator<CreateProjectDto>
{
    public CreateProjectDtoValidator()
    {
      Include(new InterfaceProjectDtoValidator());
    }
}
