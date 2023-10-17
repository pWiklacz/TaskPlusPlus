namespace TaskPlusPlus.Application.Specifications.Category;
internal class CategoriesByUserIdSpecification : Specification<Domain.Entities.Category>
{
    public CategoriesByUserIdSpecification(string userId)
        : base(category => 
            category.UserId == userId || category.UserId == Domain.Entities.Category.SystemOwner)
    {
    }

    public CategoriesByUserIdSpecification(ulong id, string userId)
        : base(category => 
            (category.UserId == userId || category.UserId == Domain.Entities.Category.SystemOwner) 
                           && category.Id == id)

    {
    }
}
