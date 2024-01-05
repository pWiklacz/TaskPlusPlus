import { Time } from '@angular/common';
import { Component, Input, OnInit, signal } from '@angular/core';
import { TaskDto } from 'src/app/shared/models/task/TaskDto';
import { TaskService } from '../task.service';
import { MessageService } from 'primeng/api';
import { HttpErrorResponse } from '@angular/common/http';
import { BsModalRef, BsModalService, ModalOptions } from 'ngx-bootstrap/modal';
import { EditTaskComponent } from '../edit-task/edit-task.component';

@Component({
  selector: 'app-task-item',
  templateUrl: './task-item.component.html',
  styleUrls: ['./task-item.component.scss']
})
export class TaskItemComponent implements OnInit {
  @Input() task?: TaskDto;
  public time = signal<string>('');
  bsModalRef?: BsModalRef;

  constructor(private taskService: TaskService,
    private messageService: MessageService,
    private modalService: BsModalService) {}

  ngOnInit(): void {
    if(this.task?.dueTime)
    {
      this.time.update(() =>this.formatTime(this.task?.dueTime!))
    }
  }

  complete()
  {
    this.taskService.changeCompleteStatus(this.task!.isCompleted, this.task!.id).subscribe({
      next: (response: any) => {
        this.taskService.updateTask(this.task!);
        this.messageService.add({ severity: 'success', summary: 'Success', detail: response.message, life: 3000 });
      },
      error: (err: HttpErrorResponse) => {
        this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Problem with compliting a task', life: 3000 });
      }
    })
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

  formatTime(inputTime: string): string {
    const [hours, minutes] = inputTime.split(':');
    return `${hours}:${minutes}`;
  }

  openEditTaskModal() {
    const initialState: ModalOptions = {
      initialState: {
       task: this.task
      },
      backdrop: 'static',
      class: 'modal-dialog-centered'
    }
    this.bsModalRef = this.modalService.show(EditTaskComponent, initialState);
  }
}

