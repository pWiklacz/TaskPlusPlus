import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AddTagComponent } from './add-tag/add-tag.component';
import { ReactiveFormsModule } from '@angular/forms';
import { ColorPickerModule } from 'primeng/colorpicker';
import { SharedModule } from 'primeng/api';


@NgModule({
  declarations: [
    AddTagComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    ReactiveFormsModule,
    ColorPickerModule,
  ],
  exports: [
    AddTagComponent
  ]
})
export class TagModule { }
