using TaskPlusPlus.Application.DTOs.Task;
using TaskPlusPlus.Application.Messaging;

namespace TaskPlusPlus.Application.Features.Tasks.Queries.GetCalendarTasksInMonth;

public record GetCalendarTasksInMonthQuery(int Month, int Year) : IQuery<List<CalendarTaskDto>>;
