import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AddProjectComponent } from './add-project/add-project.component';
import { ReactiveFormsModule } from '@angular/forms';
import { FormsModule } from '@angular/forms';
import { NgSelectModule } from '@ng-select/ng-select';
import { CalendarModule } from 'primeng/calendar';
import { SharedModule } from "../shared/shared.module";
import { ToggleButtonModule } from 'primeng/togglebutton';
import { ProjectItemComponent } from './project-item/project-item.component';
import { TaskModule } from '../task/task.module';



@NgModule({
  declarations: [
    AddProjectComponent,
    ProjectItemComponent
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    SharedModule,
    FormsModule,
    CalendarModule,
    NgSelectModule,
    ToggleButtonModule,
    TaskModule
  ],
  exports: [
    ProjectItemComponent
  ]
})
export class ProjectModule { }
