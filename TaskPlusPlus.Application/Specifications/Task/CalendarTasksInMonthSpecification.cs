
using TaskPlusPlus.Domain.ValueObjects;

namespace TaskPlusPlus.Application.Specifications.Task;
internal class CalendarTasksInMonthSpecification : Specification<Domain.Entities.Task>
{
    public CalendarTasksInMonthSpecification(DueDate firstDate, DueDate lastDate, string userId)
    : base(task =>
        (task.DueDate != null && task.DueDate >= firstDate && task.DueDate <= lastDate)
        && task.UserId == userId)
    {
        AddOrderBy(t => t.DueTime!);
    }
}
  