import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DashboardComponent } from './dashboard.component';
import { SharedModule } from '../shared/shared.module';
import { DashboardRoutingModule } from './dashboard-routing.module';
import { TodayComponent } from './today/today.component';
import { CalendarComponent } from './calendar/calendar.component';
import { ProjectsComponent } from './projects/projects.component';
import { InboxComponent } from './inbox/inbox.component';
import { NextActionsComponent } from './next-actions/next-actions.component';
import { WaitingForComponent } from './waiting-for/waiting-for.component';
import { SomedayMaybeComponent } from './someday-maybe/someday-maybe.component';
import { TaskModule } from '../task/task.module';
import { ProjectModule } from '../project/project.module';



@NgModule({
  declarations: [
    DashboardComponent,
    TodayComponent,
    CalendarComponent,
    ProjectsComponent,
    InboxComponent,
    NextActionsComponent,
    WaitingForComponent,
    SomedayMaybeComponent,
    
  ],
  imports: [
    CommonModule,
    SharedModule,
    DashboardRoutingModule,
    TaskModule,
    ProjectModule
  ],
  exports: [
  ]
})
export class DashboardModule { }
