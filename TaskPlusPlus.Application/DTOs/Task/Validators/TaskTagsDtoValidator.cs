using FluentValidation;
using TaskPlusPlus.Application.Contracts.Persistence;
using TaskPlusPlus.Application.DTOs.Tag;
using TaskPlusPlus.Application.DTOs.Task.IDto;
using TaskPlusPlus.Domain.ValueObjects.Tag;

namespace TaskPlusPlus.Application.DTOs.Task.Validators;
internal class TaskTagsDtoValidator : AbstractValidator<ITaskTagsDto>
{
    public TaskTagsDtoValidator(IUnitOfWork unitOfWork)
    {
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
                    var tagExists = await unitOfWork.Repository<Domain.Entities.Tag, TagId>()
                        .ExistsByIdAsync(tag.Id);
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
