import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AddTagComponent } from './add-tag/add-tag.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ColorPickerModule } from 'primeng/colorpicker';
import { TagListComponent } from './tag-list/tag-list.component';
import { TagItemComponent } from './tag-item/tag-item.component';
import { NgSelectModule } from '@ng-select/ng-select';
import { SharedModule } from '../shared/shared.module';
import { EditTagComponent } from './edit-tag/edit-tag.component';


@NgModule({
  declarations: [
    AddTagComponent,
    TagListComponent,
    TagItemComponent,
    EditTagComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    ReactiveFormsModule,
    ColorPickerModule,
    FormsModule,
    NgSelectModule,
  ],
  exports: [
    AddTagComponent,
    TagItemComponent
  ]
})
export class TagModule { }
