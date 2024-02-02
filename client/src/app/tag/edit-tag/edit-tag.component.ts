import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { ThemeService } from 'src/app/core/services/theme.service';
import { TagDto } from 'src/app/shared/models/tag/TagDto';
import { TagService } from '../tag.service';
import { MessageService } from 'primeng/api';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-edit-tag',
  templateUrl: './edit-tag.component.html',
  styleUrls: ['./edit-tag.component.scss']
})
export class EditTagComponent implements OnInit {
  tag?: TagDto;
  editTagForm!: FormGroup
  colorPickerPanelBgColor!: string;
  isFavorite?: boolean;
  isEdited = false;

  constructor(public bsModalRef: BsModalRef,
    private themeService: ThemeService,
    private tagService: TagService,
    private messageService: MessageService) { }

  ngOnInit() {
    this.colorPickerPanelBgColor = this.themeService.getColorPickerPanelBgColor();
    document.documentElement.style.setProperty('--some-var', this.colorPickerPanelBgColor);
    this.isFavorite = this.tag?.isFavorite;
    this.editTagForm = new FormGroup({
      name: new FormControl(this.tag!.name, Validators.required),
      color: new FormControl(this.tag?.colorHex),
      isFavorite: new FormControl(this.tag?.isFavorite)
    });

    this.editTagForm.valueChanges.subscribe((val) => {
      if (val.name !== this.tag?.name
        || val.color !== this.tag?.colorHex
        || val.isFavorite !== this.tag?.isFavorite) {
        this.isEdited = true;
      }
      else this.isEdited = false;
    })
  }

  changeIsFavoriteStatus() {
    this.isFavorite = !this.isFavorite;
    this.editTagForm.get('isFavorite')?.setValue(this.isFavorite);
  }

  onSubmit() {
    const formValues = this.editTagForm.value;

    const updatedTag: TagDto = {
      name: formValues.name!,
      isFavorite: this.isFavorite!,
      colorHex: formValues.color!,
      id: this.tag?.id!,
    }

    this.tagService.putTag(updatedTag).subscribe({
      next: (response: any) => {
        this.messageService.add({ severity: 'success', summary: 'Success', detail: response.message, life: 3000 });
        this.tagService.updateTag(updatedTag);
      },
      error: (err: HttpErrorResponse) => {
        this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Problem with editing category', life: 3000 });
      }
    })
    this.bsModalRef.hide()
  }
}
