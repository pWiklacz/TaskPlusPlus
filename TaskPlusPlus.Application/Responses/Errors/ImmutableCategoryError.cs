using TaskPlusPlus.Domain.Errors;

namespace TaskPlusPlus.Application.Responses.Errors;
internal class ImmutableCategoryError : BaseError
{
    public ImmutableCategoryError()
        :base(400, "You can't modify system category.")
    {
        
    }
}
