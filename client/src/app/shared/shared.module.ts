import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { ReplaceSpacesPipe } from './pipes/replace-spaces.pipe';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { EnumToArrayPipe } from './pipes/enum-to-array.pipe';
import { TieredMenuModule } from 'primeng/tieredmenu';
import { DeleteConfirmationModalComponent } from './components/delete-confirmation-modal/delete-confirmation-modal.component';

@NgModule({
  declarations: [
    ReplaceSpacesPipe,
    EnumToArrayPipe,
    DeleteConfirmationModalComponent
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    BsDropdownModule.forRoot(),
    BsDatepickerModule.forRoot(),
    TieredMenuModule
  ],
  exports:[
    ReactiveFormsModule,
    BsDropdownModule,
    ReplaceSpacesPipe,
    BsDatepickerModule,
    EnumToArrayPipe,
    TieredMenuModule
  ]
})
export class SharedModule { } 
