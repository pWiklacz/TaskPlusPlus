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
  calendarTasks = signal<Map<{ month: number, year: number }, calendarDay[]>>(new Map<{ month: number, year: number }, calendarDay[]>)
  currentMonthTasks = signal<calendarDay[]>([])

  constructor(private http: HttpClient, private categoryService: CategoryService) { }



  getCalendarTask(month: number, year: number) {
    let params = new HttpParams();
    params = params.append('month', month);
    params = params.append('year', year);

    return this.http.get<ApiResponse<CalendarTaskDto[]>>(this.apiUrl + 'Task/getCalendarTasks', { params });
  }

  generateCalendar(date: Date) {

    this.getCalendarTask(date.getMonth() + 1, date.getFullYear())
      .subscribe({
        next: (response) => {
          const allCategories = [...this.categoryService.userCategories(), ...this.categoryService.systemCategories]
          const categoryColorMap = new Map<number, string>();
          allCategories.forEach(category => {
            categoryColorMap.set(category.id, category.colorHex);
          });

          const tasksWithCategoryColors = response.value.map(task => ({
            ...task,
            categoryColor: categoryColorMap.get(task.categoryId) || 'defaultColor',
          }));

          this.generateMonth(date, tasksWithCategoryColors);
        },
        error: (err: HttpErrorResponse) => {
          console.log(err);
        }
      })

  }

  generateMonth(date: Date, calendarTasks: CalendarTaskDto[]) {
    console.log(calendarTasks)
    const daysInMonth: calendarDay[] = [];

    const firstDayOfMonth = new Date(date.getFullYear(), date.getMonth(), 1);

    const lastDayOfMonth = new Date(date.getFullYear(), date.getMonth() + 1, 0);

    const lastDayOfPrevMonth = new Date(date.getFullYear(), date.getMonth(), 0);

    const daysInPrevMonth = lastDayOfPrevMonth.getDate();

    const startIndex = firstDayOfMonth.getDay();


    for (let i = daysInPrevMonth - startIndex + 1; i <= daysInPrevMonth; i++) {
      const prevDate = new Date(lastDayOfPrevMonth.getFullYear(), lastDayOfPrevMonth.getMonth(), i);

      const tasks = calendarTasks.filter(obj => {
        const dueDateFromString = new Date(obj.dueDate);
        return (
          dueDateFromString.getFullYear() === prevDate.getFullYear() &&
          dueDateFromString.getMonth() === prevDate.getMonth() &&
          dueDateFromString.getDate() === prevDate.getDate()
        );
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
      const tasks = calendarTasks.filter(obj => {
        const dueDateFromString = new Date(obj.dueDate);
        return (
          dueDateFromString.getFullYear() === currentDate.getFullYear() &&
          dueDateFromString.getMonth() === currentDate.getMonth() &&
          dueDateFromString.getDate() === currentDate.getDate()
        );
      })
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
      const tasks = calendarTasks.filter(obj => {
        const dueDateFromString = new Date(obj.dueDate);
        return (
          dueDateFromString.getFullYear() === nextDate.getFullYear() &&
          dueDateFromString.getMonth() === nextDate.getMonth() &&
          dueDateFromString.getDate() === nextDate.getDate()
        );
      })
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
  }
}
