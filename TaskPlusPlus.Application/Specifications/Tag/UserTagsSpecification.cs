namespace TaskPlusPlus.Application.Specifications.Tag;
internal class UserTagsSpecification : Specification<Domain.Entities.Tag>
{
    public UserTagsSpecification(string userId)
        : base(category =>
            category.UserId == userId)
    {
    }

    public UserTagsSpecification(ulong id, string userId)
        : base(category =>
            category.UserId == userId && category.Id == id)
    {
    }
}
