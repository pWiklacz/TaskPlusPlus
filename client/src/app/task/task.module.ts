import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TaskItemComponent } from './task-item/task-item.component';
import { AddTaskComponent } from './add-task/add-task.component';
import { ReactiveFormsModule } from '@angular/forms';
import { FormsModule } from '@angular/forms';
import { NgSelectModule } from '@ng-select/ng-select';
import { CalendarModule } from 'primeng/calendar';
import { SharedModule  } from "../shared/shared.module";
import { ToggleButtonModule } from 'primeng/togglebutton';
import { AddTaskTimeConflictModalComponent } from './add-task-time-conflict-modal/add-task-time-conflict-modal.component';



@NgModule({
    declarations: [
        TaskItemComponent,
        AddTaskComponent,
        AddTaskTimeConflictModalComponent
    ],
    exports: [
        TaskItemComponent
    ],
    imports: [
        CommonModule,
        ReactiveFormsModule,
        SharedModule,
        FormsModule,
        CalendarModule,
        NgSelectModule,
        ToggleButtonModule
    ]
})
export class TaskModule { }
