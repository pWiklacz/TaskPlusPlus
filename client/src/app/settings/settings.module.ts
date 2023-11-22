import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SettingsComponent } from './settings.component';
import { ThemeSwitcherComponent } from './theme-switcher/theme-switcher.component';



@NgModule({
  declarations: [
    SettingsComponent,
    ThemeSwitcherComponent
  ],
  imports: [
    CommonModule
  ],
  exports: [
    SettingsComponent,
    ThemeSwitcherComponent
  ]
})
export class SettingsModule { }
