import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { TodayComponent } from './today/today.component';
import { CalendarComponent } from './calendar/calendar.component';
import { ProjectsComponent } from './projects/projects.component';

const routes: Routes = [
  {path: 'today', component: TodayComponent},
  {path: 'calendar', component: CalendarComponent},
  {path: 'projects', component: ProjectsComponent},
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
