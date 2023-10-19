using FluentValidation;
using TaskPlusPlus.Application.Contracts.Persistence;
using TaskPlusPlus.Application.DTOs.Common.Validators;
using TaskPlusPlus.Domain.Enums;
using TaskPlusPlus.Domain.ValueObjects.Category;
using TaskPlusPlus.Domain.ValueObjects.Project;

namespace TaskPlusPlus.Application.DTOs.Task.Validators;
public class CreateTaskDtoValidator : AbstractValidator<CreateTaskDto>
{
    private const short MaxNameLength = 500;
    private const short MaxNotesLength = 10000;

    public CreateTaskDtoValidator(IUnitOfWork unitOfWork)
    {
        Include(new DueDateDtoValidator());
        Include(new NameAndNotesValidator());
        Include(new TaskPriorityDtoValidator());
        Include(new TaskEnergyDtoValidator());

        RuleFor(dto => dto.CategoryId)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .GreaterThan(0UL)
            .MustAsync(async (id, token) =>
            {
                var categoryExists = await unitOfWork.Repository<Domain.Entities.Category, CategoryId>()
                    .ExistsByIdAsync(id);
                return categoryExists;
            })
            .WithMessage("{PropertyName} does not exist.");

        RuleFor(dto => dto.ProjectId)
            .GreaterThan(0UL)
            .When(dto => dto.ProjectId.HasValue)
            .MustAsync(async (id, token) =>
            {
                var projectExist = id != null && await unitOfWork.Repository<Domain.Entities.Project, ProjectId>()
                    .ExistsByIdAsync((ProjectId)id);
                return projectExist;
            })
            .WithMessage("{PropertyName} does not exist.");

        Include(new TaskTagsDtoValidator(unitOfWork));
    }
}
