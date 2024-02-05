import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { authGuard } from './core/guards/auth.guard';
import { TagListComponent } from './tag/tag-list/tag-list.component';

const routes: Routes = [
  { path: '', component: HomeComponent },
  {
    path: 'app',
    children: [
      {
        path: 'dashboard',
        canActivate: [authGuard],
        loadChildren: () => import('./dashboard/dashboard.module').then(m => m.DashboardModule),
      },
      {
        path: 'tags',
        canActivate: [authGuard],
        component: TagListComponent
      },
      {
        path: 'settings',
        outlet: 'settings',   
        canActivate: [authGuard],
        loadChildren: () => import('./settings/settings.module').then(m => m.SettingsModule),
      }
    ]
  },
  { path: 'account', loadChildren: () => import('./account/account.module').then(m => m.AccountModule) },
  { path: '**', redirectTo: '', pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes,
    { 
      scrollPositionRestoration: "enabled",
      anchorScrolling: "enabled",
      enableTracing: false
    })],
  exports: [RouterModule]
})
export class AppRoutingModule { }
