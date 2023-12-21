using System;
using AutoMapper;
using FluentResults;
using TaskPlusPlus.Application.Contracts.Persistence;
using TaskPlusPlus.Application.DTOs.Task;
using TaskPlusPlus.Application.Messaging;
using TaskPlusPlus.Application.Models.Identity.ApplicationUser;
using TaskPlusPlus.Application.Specifications.Task;
using TaskPlusPlus.Domain.ValueObjects;
using TaskPlusPlus.Domain.ValueObjects.Task;
using Task = TaskPlusPlus.Domain.Entities.Task;

namespace TaskPlusPlus.Application.Features.Tasks.Queries.GetCalendarTasksInMonth;
internal class GetCalendarTasksInMonthQueryHandler : IQueryHandler<GetCalendarTasksInMonthQuery, List<CalendarTaskDto>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserContext _userContext;

    public GetCalendarTasksInMonthQueryHandler(IMapper mapper, IUnitOfWork unitOfWork, IUserContext userContext)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _userContext = userContext;
    }

    public async Task<Result<List<CalendarTaskDto>>> Handle(GetCalendarTasksInMonthQuery request, CancellationToken cancellationToken)
    {
        var userResult = _userContext.GetCurrentUser();
        if (userResult.IsFailed)
        {
            return userResult.ToResult();
        }

        var dates = GetTheDateRangeOfOneCalendarMonth(request.Month, request.Year);
        var userId = userResult.Value.Id;
        var firstDate = DueDate.Create(dates.Item1);
        var lastDate = DueDate.Create(dates.Item2);

        var spec = new CalendarTasksInMonthSpecification(firstDate.Value, lastDate.Value, userId);
        var tasksFromDb = await _unitOfWork.Repository<Task, TaskId>().ListAsync(spec);
        var tasksDto = _mapper.Map<List<CalendarTaskDto>>(tasksFromDb);
        return tasksDto;
    }

    static Tuple<DateOnly, DateOnly> GetTheDateRangeOfOneCalendarMonth(int month, int year)
    {
        DateTime firstDayOfGivenMonth = new DateTime(year, month, 1);

        DateTime lastDayOfGivenMonth = firstDayOfGivenMonth.AddMonths(1).AddDays(-1);

        DateTime lastDayOfPrevMonth = firstDayOfGivenMonth.AddDays(-1);

        int daysInActualMonth = DateTime.DaysInMonth(year, month);

        var startIndex = (int)firstDayOfGivenMonth.DayOfWeek;
        var lastIndex = 42 - daysInActualMonth - startIndex;

        var firstDateOfCalendar = DateOnly.FromDateTime(firstDayOfGivenMonth.AddDays(-startIndex));

        var lastDateOfCalendar = DateOnly.FromDateTime(lastDayOfGivenMonth.AddDays(+lastIndex));

        return Tuple.Create(firstDateOfCalendar, lastDateOfCalendar);
    }
}
