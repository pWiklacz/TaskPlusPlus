
using FluentValidation;
using TaskPlusPlus.Application.Contracts.Persistence.Repositories;
using TaskPlusPlus.Application.DTOs.Tag;
using TaskPlusPlus.Domain.Enums;

namespace TaskPlusPlus.Application.DTOs.Task.Validators;
public class CreateTaskDtoValidator : AbstractValidator<CreateTaskDto>
{
    private const short MaxNameLength = 500;
    private const short MaxNotesLength = 10000;

    public CreateTaskDtoValidator(
        ICategoryRepository categoryRepository, 
        IProjectRepository projectRepository,
        ITagRepository tagRepository)
    {
        RuleFor(dto => dto.Name)
            .NotEmpty().WithMessage("{PropertyName} is required")
            .NotNull()
            .MaximumLength(MaxNameLength).WithMessage($"Tag name character limit {{PropertyValue}}/{MaxNameLength}");
        
        RuleFor(dto => dto.Notes)
            .NotNull()
            .MaximumLength(MaxNameLength).WithMessage($"Tag name character limit {{PropertyValue}}/{MaxNotesLength}");

        RuleFor(dto => dto.Energy)
            .NotNull()
            .Must(e => Energy.FromName(e) != null).WithMessage("There is no Energy value name {PropertyName}");

        RuleFor(dto => dto.Priority)
            .NotNull()
            .Must(e => Priority.FromName(e) != null).WithMessage("There is no Priority value name {PropertyName}");

        RuleFor(dto => dto.DueDate)
            .GreaterThan(DateTime.Now).WithMessage("Due date cannot be in the past.\"");

        RuleFor(dto => dto.CategoryId)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .GreaterThan(0UL) 
            .MustAsync(async (id, token) =>
            {
                var categoryExists = await categoryRepository.ExistsByIdAsync(id);
                return categoryExists;
            })
            .WithMessage("{PropertyName} does not exist.");

        RuleFor(dto => dto.ProjectId)
            .GreaterThan(0UL)
            .When(dto => dto.ProjectId.HasValue)
            .MustAsync(async (id, token) =>
            {
                var projectExist = id != null && await projectRepository.ExistsByIdAsync(id);
                return projectExist;
            })
            .WithMessage("{PropertyName} does not exist.");

        RuleFor(dto => dto.Tags)
            .MustAsync(async (tags, token) =>
            {
                if (tags == null || !tags.Any())
                {
                    return true; 
                }

                var invalidTags = new List<TagDto>(); 

                foreach (var tag in tags)
                {
                    var tagExists = await tagRepository.ExistsByIdAsync(tag.Id);
                    if (!tagExists)
                    {
                        invalidTags.Add(tag); 
                    }
                }

                return !invalidTags.Any();
            })
            .WithMessage("One or more tags do not exist in the database. Invalid tags: {InvalidTags}");
    }
}
