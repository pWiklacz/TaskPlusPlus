import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomeComponent } from './home.component';
import { AppModule } from '../app.module';
import { CoreModule } from '../core/core.module';

@NgModule({
  declarations: [
    HomeComponent
  ],
  imports: [
    CommonModule,
    CoreModule
  ],
  exports:[
    HomeComponent
  ]
})
export class HomeModule { }
