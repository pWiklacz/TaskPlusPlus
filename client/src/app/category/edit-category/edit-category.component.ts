import { Component, OnInit, Input } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { ThemeService } from 'src/app/core/services/theme.service';
import { CategoryDto } from 'src/app/shared/models/category/CategoryDto';
import { CategoryService } from '../category.service';
import { MessageService } from 'primeng/api';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-edit-category',
  templateUrl: './edit-category.component.html',
  styleUrls: ['./edit-category.component.scss']
})
export class EditCategoryComponent implements OnInit {
  category?: CategoryDto;
  editCategoryForm!: FormGroup
  colorPickerPanelBgColor!: string;
  isFavorite?: boolean;
  isEdited = false;

  constructor(public bsModalRef: BsModalRef,
    private themeService: ThemeService,
    private categoryService: CategoryService,
    private messageService: MessageService) { }

  ngOnInit(): void {
    this.colorPickerPanelBgColor = this.themeService.getColorPickerPanelBgColor();
    document.documentElement.style.setProperty('--some-var', this.colorPickerPanelBgColor);
    this.isFavorite = this.category?.isFavorite;
    this.editCategoryForm = new FormGroup({
      name: new FormControl(this.category?.name, Validators.required),
      color: new FormControl(this.category?.colorHex),
      isFavorite: new FormControl(this.category?.isFavorite)
    });

    this.editCategoryForm.valueChanges.subscribe((val) => {
      if (val.name !== this.category?.name
        || val.color !== this.category?.colorHex
        || val.isFavorite !== this.category?.isFavorite) {
        this.isEdited = true;
      }
      else this.isEdited = false;
    })
  }

  changeIsFavoriteStatus() {
    this.isFavorite = !this.isFavorite;
    this.editCategoryForm.get('isFavorite')?.setValue(this.isFavorite);
  }

  onSubmit() {
    const formValues = this.editCategoryForm.value;

    const updatedCategory: CategoryDto = {
      name: formValues.name!,
      isFavorite: this.isFavorite!,
      colorHex: formValues.color!,
      isImmutable: false,
      icon: 'fa-solid fa-circle',
      id: this.category?.id!
    }

    this.categoryService.putCategory(updatedCategory).subscribe({
      next: (response: any) => {
        this.messageService.add({ severity: 'success', summary: 'Success', detail: response.message, life: 3000 });
        this.categoryService.updateCategory(updatedCategory);
      },
      error: (err: HttpErrorResponse) => {
        this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Problem with editing category', life: 3000 });
      }
    })
    this.bsModalRef.hide()
  }
}
