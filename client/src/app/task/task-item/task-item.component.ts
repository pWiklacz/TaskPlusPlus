import { Time } from '@angular/common';
import { Component, Input, OnInit } from '@angular/core';
import { TaskDto } from 'src/app/shared/models/task/TaskDto';

@Component({
  selector: 'app-task-item',
  templateUrl: './task-item.component.html',
  styleUrls: ['./task-item.component.scss']
})
export class TaskItemComponent {
  @Input() task?: TaskDto;

  formatTime(time: Time): string {
    const timeSA = time.toString().split(":", 2);
    var timeA: number[] = [];

    for (let item of timeSA) {
      let no: number = Number(item);
      timeA.push(no);
    }
    if (timeA[0] > 0 && timeA[1] > 0)
      return `${timeA[0]}h ${timeA[1]}min`;
    else if (timeA[0] == 0 && timeA[1] > 0)
      return `${timeA[1]}min`;
    else
      return `${timeA[0]}h`;
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

  formatPriorityIcon(priority: number)
  {
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
}

