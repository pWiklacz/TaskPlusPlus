import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AddCategoryComponent } from './add-category/add-category.component';
import { ReactiveFormsModule } from '@angular/forms';
import { ColorPickerModule } from 'primeng/colorpicker';
import { SharedModule } from 'primeng/api';
import { EditCategoryComponent } from './edit-category/edit-category.component';


@NgModule({
  declarations: [
    AddCategoryComponent,
    EditCategoryComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    ReactiveFormsModule,
    ColorPickerModule,
  ],
  exports: [
    AddCategoryComponent
  ]
})
export class CategoryModule { }
