using TaskPlusPlus.Domain.Entities;
using Task = TaskPlusPlus.Domain.Entities.Task;

namespace TaskPlusPlus.Application.Persistence.Contracts;

public interface ITaskRepository : IGenericRepository<Task>
{
    //TODO:: Add some methods
}