import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TaskItemComponent } from './task-item/task-item.component';



@NgModule({
  declarations: [
    TaskItemComponent
  ],
  imports: [
    CommonModule
  ],
  exports: [
    TaskItemComponent
  ]
})
export class TaskModule { }
