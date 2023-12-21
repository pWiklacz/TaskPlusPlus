import { Time } from '@angular/common';
import { Component, Input, OnInit } from '@angular/core';
import { TaskDto } from 'src/app/shared/models/task/TaskDto';

@Component({
  selector: 'app-task-item',
  templateUrl: './task-item.component.html',
  styleUrls: ['./task-item.component.scss']
})
export class TaskItemComponent implements OnInit {
  @Input() task?: TaskDto;
  time: string = '';

  ngOnInit(): void {
    if(this.task?.dueTime)
    {
      this.time = this.formatTime(this.task?.dueTime!)
    }
  }

  formatMinutes(minutes: number): string {
    if (isNaN(minutes) || minutes < 0) {
      return 'Invalid input';
    }

    const hours = Math.floor(minutes / 60);
    const remainingMinutes = minutes % 60;

    let result = '';

    if (hours > 0) {
      result += `${hours}h`;
    }

    if (remainingMinutes > 0) {
      if (result !== '') {
        result += ' ';
      }
      result += `${remainingMinutes}min`;
    }

    return result !== '' ? result : '0min';
  }

  formatEnergyIcon(energy: number) {
    switch (energy) {
      case 0: {
        return "fa-solid fa-battery-empty";
      }
      case 1: {
        return "fa-solid fa-battery-quarter";
      }
      case 2: {
        return "fa-solid fa-battery-half";
      }
      case 3: {
        return "fa-solid fa-battery-full";
      }
      default: {
        return "";
      }
    }
  }

  formatPriorityIcon(priority: number) {
    switch (priority) {
      case 1: {
        return "fa-solid fa-e";
      }
      case 2: {
        return "fa-solid fa-d";
      }
      case 3: {
        return "fa-solid fa-c";
      }
      case 4: {
        return "fa-solid fa-b";
      }
      case 5: {
        return "fa-solid fa-a";
      }
      default: {
        return "";
      }
    }
  }

  formatTime(time: Time): string {
    const timeSA = time.toString().split(":", 2);
    return `${timeSA[0]}:${timeSA[1]}`
  }
}

