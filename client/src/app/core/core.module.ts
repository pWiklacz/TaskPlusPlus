import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavBarUnloggedComponent } from './nav-bar-unlogged/nav-bar-unlogged.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { RouterModule } from '@angular/router';
import { NavBarLoggedComponent } from './nav-bar-logged/nav-bar-logged.component';
import { SharedModule } from '../shared/shared.module';



@NgModule({
  declarations: [
    NavBarUnloggedComponent,
    NavBarLoggedComponent
  ],
  imports: [
    CommonModule,
    FontAwesomeModule,
    RouterModule,
    SharedModule
  ],
  exports: [
    NavBarUnloggedComponent,
    NavBarLoggedComponent
  ]
})
export class CoreModule { }
