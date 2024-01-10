import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { ProfileSettingsComponent } from './profile-settings/profile-settings.component';
import { AccountSettingsComponent } from './account-settings/account-settings.component';
import { SecuritySettingsComponent } from './security-settings/security-settings.component';
import { GeneralSettingsComponent } from './general-settings/general-settings.component';
import { ThemeSwitcherComponent } from './theme-switcher/theme-switcher.component';

const routes: Routes = [
  { path: 'profile', component: ProfileSettingsComponent },
  { path: 'account', component: AccountSettingsComponent },
  { path: 'security', component: SecuritySettingsComponent },
  { path: 'general', component: GeneralSettingsComponent },
  { path: 'theme', component: ThemeSwitcherComponent },
  { path: '**', redirectTo: 'profile', pathMatch: 'full' },
  { path: '', redirectTo: 'profile', pathMatch: 'full' }
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
export class SettingsRoutingModule { }
