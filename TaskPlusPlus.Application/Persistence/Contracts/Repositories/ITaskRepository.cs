using TaskPlusPlus.Domain.Entities;
using TaskPlusPlus.Domain.ValueObjects.Task;
using Task = TaskPlusPlus.Domain.Entities.Task;

namespace TaskPlusPlus.Application.Persistence.Contracts.Repositories;

public interface ITaskRepository : IGenericRepository<Task, TaskId>
{
    //TODO:: Add some methods
}