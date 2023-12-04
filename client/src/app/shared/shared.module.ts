import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { ReplaceSpacesPipe } from './pipes/replace-spaces.pipe';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';

@NgModule({
  declarations: [
    ReplaceSpacesPipe
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    BsDropdownModule.forRoot(),
    BsDatepickerModule.forRoot(),
  ],
  exports:[
    ReactiveFormsModule,
    BsDropdownModule,
    ReplaceSpacesPipe,
    BsDatepickerModule
  ]
})
export class SharedModule { } 
