import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { TodayComponent } from './today/today.component';
import { CalendarComponent } from './calendar/calendar.component';
import { ProjectsComponent } from './projects/projects.component';
import { DashboardComponent } from './dashboard.component';
import { InboxComponent } from './inbox/inbox.component';
import { NextActionsComponent } from './next-actions/next-actions.component';
import { WaitingForComponent } from './waiting-for/waiting-for.component';

const routes: Routes = [
  {path: 'today', component: TodayComponent},
  {path: 'calendar', component: CalendarComponent},
  {path: 'projects', component: ProjectsComponent},
  {path: 'inbox', component: InboxComponent},
  {path: 'nextactions', component: NextActionsComponent},
  {path: 'waitingfor', component: WaitingForComponent},
  {path: 'someday_maybe', component: WaitingForComponent},
  {path: 'category/:id', component: DashboardComponent},
  { path: '**', redirectTo: 'today', pathMatch: 'full' },
  { path: '', redirectTo: 'today', pathMatch: 'full' }
]

@NgModule({
  declarations: [], 
  imports: [
    CommonModule,
    RouterModule.forChild(routes)
  ],
  exports: [
    RouterModule
  ]
})
export class DashboardRoutingModule { }
