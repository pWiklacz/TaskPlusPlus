using TaskPlusPlus.Application.Contracts.Persistence.Repositories;
using TaskPlusPlus.Domain.Entities;
using TaskPlusPlus.Domain.ValueObjects.Tag;

namespace TaskPlusPlus.Persistence.Repositories;

internal sealed class TagRepository :  GenericRepository<Tag, TagId>, ITagRepository
{
    public TagRepository(TaskPlusPlusDbContext dbContext) : base(dbContext)
    {
    }
}