import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SettingsComponent } from './settings.component';
import { ThemeSwitcherComponent } from './theme-switcher/theme-switcher.component';
import { AccountSettingsComponent } from './account-settings/account-settings.component';
import { SecuritySettingsComponent } from './security-settings/security-settings.component';
import { ProfileSettingsComponent } from './profile-settings/profile-settings.component';
import { GeneralSettingsComponent } from './general-settings/general-settings.component';
import { SettingsRoutingModule } from './settings-routing.module';
import { SharedModule } from 'primeng/api';
import { CoreModule } from '../core/core.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';


@NgModule({
  declarations: [
    SettingsComponent,
    ThemeSwitcherComponent,
    
    AccountSettingsComponent,
    SecuritySettingsComponent,
    ProfileSettingsComponent,
    GeneralSettingsComponent
  ],
  imports: [
    CommonModule,
    SettingsRoutingModule,
    ReactiveFormsModule,
    SharedModule,
    CoreModule,
    FormsModule
  ],
  exports: [
    SettingsComponent,
    ThemeSwitcherComponent
  ]
})
export class SettingsModule { }
