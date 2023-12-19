import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { ThemeService } from 'src/app/core/services/theme.service';
import { CategoryDto} from 'src/app/shared/models/category/CategoryDto';
import { CategoryService } from '../category.service';
import { MessageService } from 'primeng/api';
import { HttpErrorResponse } from '@angular/common/http';
import { CreateCategoryDto } from 'src/app/shared/models/category/CreateCategoryDto';

@Component({
  selector: 'app-add-category',
  templateUrl: './add-category.component.html',
  styleUrls: ['./add-category.component.scss']
})
export class AddCategoryComponent implements OnInit {
  addCategoryForm!: FormGroup;
  colorPickerPanelBgColor!: string;
  isFavorite: boolean = false;

  constructor(public bsModalRef: BsModalRef,
    private themeService: ThemeService,
    private categoryService: CategoryService,
    private messageService: MessageService) { }

  ngOnInit() {
    this.colorPickerPanelBgColor = this.themeService.getColorPickerPanelBgColor();
    document.documentElement.style.setProperty('--some-var', this.colorPickerPanelBgColor);
    this.addCategoryForm = new FormGroup({
      name: new FormControl('', Validators.required),
      color: new FormControl('#593196'),
    });
  }

  changeIsFavoriteStatus() {
    this.isFavorite = !this.isFavorite;
  }
  
  onSubmit() {
    const formValues = this.addCategoryForm.value;
    const createdCategory: CreateCategoryDto = {
      name: formValues.name!,
      isFavorite: this.isFavorite,
      colorHex: formValues.color!,
      icon: 'fa-solid fa-circle'
    }

    this.categoryService.postCategory(createdCategory).subscribe({
      next: (response: any) => {
        this.messageService.add({ severity: 'success', summary: 'Success', detail: response.message, life: 3000 });
        const category: CategoryDto = {
          id: response.value,
          name: createdCategory.name,
          isFavorite: createdCategory.isFavorite,
          isImmutable: false,
          colorHex: createdCategory.colorHex,
          icon: createdCategory.icon
        }
        this.categoryService.addCategory(category);
      },
      error: (err: HttpErrorResponse) => {      
        this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Problem with creating category', life: 3000 });
      }
    })

    this.bsModalRef.hide()
  }
}
