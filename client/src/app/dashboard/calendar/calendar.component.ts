import { AfterViewInit, Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { CalendarService } from './calendar.service';

@Component({
  selector: 'app-calendar',
  templateUrl: './calendar.component.html',
  styleUrls: ['./calendar.component.scss']
})
export class CalendarComponent implements OnInit{
  date: Date = new Date();
  today: Date = new Date();
  months: string[] = [
    'January', 'February', 'March', 'April', 'May', 'June',
    'July', 'August', 'September', 'October', 'November', 'December'
  ];
  weekdays: string[] = ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'];

  constructor(public calendarService: CalendarService){}

  ngOnInit(): void {
   this.calendarService.generateCalendar(this.date)
  }

  incrementMonth()
  {
    this.date.setMonth(this.date.getMonth() + 1)
    this.calendarService.generateCalendar(this.date)
  }

  decrementMonth()
  {
    this.date.setMonth(this.date.getMonth() -1)
    this.calendarService.generateCalendar(this.date)
  }
}
