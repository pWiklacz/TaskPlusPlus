import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavBarUnloggedComponent } from './nav-bar-unlogged/nav-bar-unlogged.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { RouterModule } from '@angular/router';
import { NavBarLoggedComponent } from './nav-bar-logged/nav-bar-logged.component';



@NgModule({
  declarations: [
    NavBarUnloggedComponent,
    NavBarLoggedComponent
  ],
  imports: [
    CommonModule,
    FontAwesomeModule,
    RouterModule
  ],
  exports: [
    NavBarUnloggedComponent,
    NavBarLoggedComponent
  ]
})
export class CoreModule { }
