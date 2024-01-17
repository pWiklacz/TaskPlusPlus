import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomeComponent } from './home.component';
import { AppModule } from '../app.module';
import { CoreModule } from '../core/core.module';
import { AnimateOnScrollModule } from 'primeng/animateonscroll';
import { RouterModule } from '@angular/router';

@NgModule({
  declarations: [
    HomeComponent
  ],
  imports: [
    CommonModule,
    CoreModule,
    AnimateOnScrollModule,
    RouterModule
  ],
  exports:[
    HomeComponent
  ]
})
export class HomeModule { }
