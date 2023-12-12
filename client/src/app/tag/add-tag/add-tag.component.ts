import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { TagService } from '../tag.service';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { ThemeService } from 'src/app/core/services/theme.service';
import { MessageService } from 'primeng/api';
import { CreateTagDto } from 'src/app/shared/models/tag/CreateTagDto';
import { TagDto } from 'src/app/shared/models/tag/TagDto';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-add-tag',
  templateUrl: './add-tag.component.html',
  styleUrls: ['./add-tag.component.scss']
})
export class AddTagComponent implements OnInit {
  addTagForm!: FormGroup;
  colorPickerPanelBgColor!: string;
  isFavorite: boolean = false;

  constructor(public bsModalRef: BsModalRef,
    private themeService: ThemeService,
    private tagService: TagService,
    private messageService: MessageService) { }

  ngOnInit() {
    this.colorPickerPanelBgColor = this.themeService.getColorPickerPanelBgColor();
    document.documentElement.style.setProperty('--some-var', this.colorPickerPanelBgColor);
    this.addTagForm = new FormGroup({
      name: new FormControl('', Validators.required),
      color: new FormControl('#593196'),
    });
  }

  changeIsFavoriteStatus() {
    this.isFavorite = !this.isFavorite;
  }

  onSubmit() {
    const formValues = this.addTagForm.value;
    const createdTag: CreateTagDto = {
      name: formValues.name!,
      isFavorite: this.isFavorite,
      colorHex: formValues.color!
    }

    this.tagService.postTag(createdTag).subscribe({
      next: (response: any) => {
        this.messageService.add({ severity: 'success', summary: 'Success', detail: response.message, life: 3000 });
        const category: TagDto = {
          id: response.value,
          name: createdTag.name,
          isFavorite: createdTag.isFavorite,
          colorHex: createdTag.colorHex,
        }
        this.tagService.addTag(category);
      },
      error: (err: HttpErrorResponse) => {
        this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Problem with creating tag', life: 3000 });
      }
    })

    this.bsModalRef.hide()
  }
}
