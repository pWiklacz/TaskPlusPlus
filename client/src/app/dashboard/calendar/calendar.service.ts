import { HttpClient, HttpErrorResponse, HttpParams } from '@angular/common/http';
import { Injectable, signal } from '@angular/core';
import { map } from 'rxjs';
import { CategoryService } from 'src/app/category/category.service';
import { ApiResponse } from 'src/app/shared/models/ApiResponse';
import { CalendarTaskDto } from 'src/app/shared/models/task/CalendarTaskDto';
import { environment } from 'src/environments/environment';

interface calendarDay {
  date: Date;
  dayNumber: number;
  isCurrentMonth: boolean;
  isToday: boolean;
  tasks: CalendarTaskDto[]
}

@Injectable({
  providedIn: 'root'
})
export class CalendarService {
  apiUrl = environment.apiUrl;
  today: Date = new Date();
  constructor(private http: HttpClient, private categoryService: CategoryService) { }
  calendarTasks = signal<Map<{ month: number, year: number }, calendarDay[]>>(new Map<{ month: number, year: number }, calendarDay[]>)
  currentMonthTasks = signal<calendarDay[]>([])

  getCalendarTask(month: number, year: number) {
    let params = new HttpParams();
    params = params.append('month', month);
    params = params.append('year', year);

    return this.http.get<ApiResponse<CalendarTaskDto[]>>(this.apiUrl + 'Task/getCalendarTasks', { params });
  }


  generateCalendar(date: Date) {

    const daysInMonth: calendarDay[] = [];

    const firstDayOfMonth = new Date(date.getFullYear(), date.getMonth(), 1);

    const lastDayOfMonth = new Date(date.getFullYear(), date.getMonth() + 1, 0);

    const lastDayOfPrevMonth = new Date(date.getFullYear(), date.getMonth(), 0);

    const daysInPrevMonth = lastDayOfPrevMonth.getDate();

    const startIndex = firstDayOfMonth.getDay();

    let calendarTasks: CalendarTaskDto[] = [];

    this.getCalendarTask(date.getMonth() + 1, date.getFullYear())
      .subscribe({
        next: (response) => {
          calendarTasks = response.value
        },
        error: (err: HttpErrorResponse) => {
          console.log(err);
        }
      })

      console.log(calendarTasks)

    for (let i = daysInPrevMonth - startIndex + 1; i <= daysInPrevMonth; i++) {
      const prevDate = new Date(lastDayOfPrevMonth.getFullYear(), lastDayOfPrevMonth.getMonth(), i);
      // console.log(prevDate)
      // console.log(calendarTasks)
      const tasks = calendarTasks.filter(obj => {
        obj.dueDate.getTime() === prevDate.getTime()
        // console.log(obj.dueDate.getTime())
        // console.log(prevDate.getTime())
      })
        .map(obj => ({ ...obj }));

      daysInMonth.push({
        date: prevDate,
        dayNumber: i,
        isCurrentMonth: false,
        isToday: prevDate.toDateString() === this.today.toDateString(),
        tasks: tasks
      });
    }

    for (let i = 1; i <= lastDayOfMonth.getDate(); i++) {
      const currentDate = new Date(firstDayOfMonth.getFullYear(), firstDayOfMonth.getMonth(), i);
      const isCurrentMonth = currentDate.getMonth() === date.getMonth();
      const tasks = calendarTasks.filter(obj => obj.dueDate.getTime() === currentDate.getTime())
        .map(obj => ({ ...obj }));

      daysInMonth.push({
        date: currentDate,
        dayNumber: i,
        isCurrentMonth: isCurrentMonth,
        isToday: currentDate.toDateString() === this.today.toDateString(),
        tasks: tasks
      });
    }

    const daysToAdd = 42 - daysInMonth.length;

    for (let i = 1; i <= daysToAdd; i++) {
      const nextDate = new Date(lastDayOfMonth.getFullYear(), lastDayOfMonth.getMonth() + 1, i);
      const tasks = calendarTasks.filter(obj => obj.dueDate.getTime() === nextDate.getTime())
        .map(obj => ({ ...obj }));
      daysInMonth.push({
        date: nextDate,
        dayNumber: i,
        isCurrentMonth: false,
        isToday: nextDate.toDateString() === this.today.toDateString(),
        tasks: tasks
      });
    }

    this.currentMonthTasks.set(daysInMonth)
    console.log(this.currentMonthTasks())
  }
}
