import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { ReplaceSpacesPipe } from './pipes/replace-spaces.pipe';


@NgModule({
  declarations: [
    ReplaceSpacesPipe
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    BsDropdownModule.forRoot()
  ],
  exports:[
    ReactiveFormsModule,
    BsDropdownModule,
    ReplaceSpacesPipe
  ]
})
export class SharedModule { }
