using FluentResults;

namespace TaskPlusPlus.Domain.Errors;

internal class ItemNotFoundError : Error
{
    public ItemNotFoundError(Type itemType, string itemName)
    : base($"{itemType} with name {itemType} was not found.")
    {
        
    }
}