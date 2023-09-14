using TaskPlusPlus.Application.Contracts.Persistence.Repositories;
using TaskPlusPlus.Domain.ValueObjects.Task;
using Task = TaskPlusPlus.Domain.Entities.Task;

namespace TaskPlusPlus.Persistence.Repositories;

internal sealed class TaskRepository : GenericRepository<Task,TaskId>, ITaskRepository
{
    public TaskRepository(TaskPlusPlusDbContext dbContext) : base(dbContext)
    {
    }
}