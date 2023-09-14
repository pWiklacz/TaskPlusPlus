using TaskPlusPlus.Domain.ValueObjects.Task;
using Task = TaskPlusPlus.Domain.Entities.Task;

namespace TaskPlusPlus.Application.Contracts.Persistence.Repositories;

public interface ITaskRepository : IGenericRepository<Task, TaskId>
{
    //TODO:: Add some methods
}