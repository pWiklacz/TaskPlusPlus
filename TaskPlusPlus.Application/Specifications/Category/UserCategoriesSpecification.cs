namespace TaskPlusPlus.Application.Specifications.Category;
internal class UserCategoriesSpecification : Specification<Domain.Entities.Category>
{
    public UserCategoriesSpecification(string userId)
        : base(category =>
            category.UserId == userId)
    {
    }

    public UserCategoriesSpecification(ulong id, string userId)
        : base(category =>
            (category.UserId == userId || category.UserId == Domain.Entities.Category.SystemOwner)
                           && category.Id == id)

    {
    }
}
