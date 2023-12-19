import { Component } from '@angular/core';

@Component({
  selector: 'app-calendar',
  templateUrl: './calendar.component.html',
  styleUrls: ['./calendar.component.scss']
})
export class CalendarComponent {
  date: Date = new Date();
  today: Date = new Date();
  months: string[] = [
    'January', 'February', 'March', 'April', 'May', 'June',
    'July', 'August', 'September', 'October', 'November', 'December'
  ];
  weekdays: string[] = ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'];
  
  generateCalendar(): any[] {
    this.date = new Date('12/12/2023');
    const daysInMonth = [];
    const firstDayOfMonth = new Date(this.date.getFullYear(), this.date.getMonth(), 1);
    const lastDayOfMonth = new Date(this.date.getFullYear(), this.date.getMonth() + 1, 0);
    const lastDayOfPrevMonth = new Date(this.date.getFullYear(), this.date.getMonth(), 0);
    const daysInPrevMonth = lastDayOfPrevMonth.getDate();

    // Pobierz indeks pierwszego dnia bieżącego miesiąca w tygodniu
    const startIndex = firstDayOfMonth.getDay();

    // Dodaj dni z poprzedniego miesiąca
    for (let i = daysInPrevMonth - startIndex + 1; i <= daysInPrevMonth; i++) {
      const prevDate = new Date(lastDayOfPrevMonth.getFullYear(), lastDayOfPrevMonth.getMonth(), i);
      daysInMonth.push({
        date: prevDate,
        dayNumber: i,
        isCurrentMonth: false,
        isToday: prevDate.toDateString() === this.today.toDateString()
      });
    }

    // Dodaj dni z bieżącego miesiąca
    for (let i = 1; i <= lastDayOfMonth.getDate(); i++) {
      const currentDate = new Date(firstDayOfMonth.getFullYear(), firstDayOfMonth.getMonth(), i);
      const isCurrentMonth = currentDate.getMonth() === this.date.getMonth();

      daysInMonth.push({
        date: currentDate,
        dayNumber: i,
        isCurrentMonth: isCurrentMonth,
        isToday: currentDate.toDateString() === this.today.toDateString()
      });
    }

    // Dodaj dni z następnego miesiąca, aby uzyskać 42 dni
    const daysToAdd = 42 - daysInMonth.length;
    for (let i = 1; i <= daysToAdd; i++) {
      const nextDate = new Date(lastDayOfMonth.getFullYear(), lastDayOfMonth.getMonth() + 1, i);
      daysInMonth.push({
        date: nextDate,
        dayNumber: i,
        isCurrentMonth: false,
        isToday: nextDate.toDateString() === this.today.toDateString()
      });
    }

    return daysInMonth;
  }
}
