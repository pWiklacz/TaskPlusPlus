using FluentValidation;
using TaskPlusPlus.Application.Contracts.Persistence;
using TaskPlusPlus.Domain.ValueObjects.Project;

namespace TaskPlusPlus.Application.DTOs.Task.Validators;
internal class ChangeTaskProjectDtoValidator : AbstractValidator<ChangeTaskProjectDto>
{
    public ChangeTaskProjectDtoValidator(IUnitOfWork unitOfWork)
    {

        RuleFor(dto => dto.ProjectId)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .MustAsync(async (id, token) =>
            {
                var projectExist = await unitOfWork.Repository<Domain.Entities.Project, ProjectId>()
                    .ExistsByIdAsync((ProjectId)id);
                return projectExist;
            })
            .WithMessage("{PropertyName} does not exist.");

    }
}
