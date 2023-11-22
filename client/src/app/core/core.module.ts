import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavBarUnloggedComponent } from './nav-bar-unlogged/nav-bar-unlogged.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { RouterModule } from '@angular/router';
import { NavBarLoggedComponent } from './nav-bar-logged/nav-bar-logged.component';
import { SharedModule } from '../shared/shared.module';
import { ToastModule } from 'primeng/toast';
import { ThemeSwitcherComponent } from '../settings/theme-switcher/theme-switcher.component';
import { SideNavComponent } from './side-nav/side-nav.component';
import { SettingsModule } from '../settings/settings.module';
import { NgxSpinnerModule } from 'ngx-spinner';



@NgModule({
  declarations: [
    NavBarUnloggedComponent,
    NavBarLoggedComponent,
    SideNavComponent
  ],
  imports: [
    CommonModule,
    FontAwesomeModule,
    RouterModule,
    SharedModule,
    NgxSpinnerModule
  ],
  exports: [
    NavBarUnloggedComponent,
    NavBarLoggedComponent,
    ToastModule,
    SideNavComponent,
    NgxSpinnerModule
  ]
})
export class CoreModule { }
