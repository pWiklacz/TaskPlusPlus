import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavBarUnloggedComponent } from './nav-bar-unlogged/nav-bar-unlogged.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { RouterModule } from '@angular/router';
import { NavBarLoggedComponent } from './nav-bar-logged/nav-bar-logged.component';
import { SharedModule } from '../shared/shared.module';
import { ToastModule } from 'primeng/toast';
import { SideNavComponent } from './side-nav/side-nav.component';
import { NgxSpinnerModule } from 'ngx-spinner';
import { AvatarModule } from 'primeng/avatar';

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
    NgxSpinnerModule,
    AvatarModule

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
