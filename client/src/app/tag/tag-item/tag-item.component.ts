import { Component, Input } from '@angular/core';
import { BsModalRef, BsModalService, ModalOptions } from 'ngx-bootstrap/modal';
import { TagDto } from 'src/app/shared/models/tag/TagDto';
import { EditTagComponent } from '../edit-tag/edit-tag.component';
import { DeleteConfirmationModalComponent } from 'src/app/shared/components/delete-confirmation-modal/delete-confirmation-modal.component';
import { TagService } from '../tag.service';
import { MessageService } from 'primeng/api';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-tag-item',
  templateUrl: './tag-item.component.html',
  styleUrls: ['./tag-item.component.scss']
})
export class TagItemComponent {
  @Input() tag?: TagDto;
  bsModalRef?: BsModalRef;

  constructor(private modalService: BsModalService,
    private tagService: TagService,
    private messageService: MessageService) { }

  openEditTagModal() {
    const initialState: ModalOptions = {
      initialState: {
        tag: this.tag
      },
      backdrop: 'static',
      class: 'modal-dialog-centered'
    }
    this.bsModalRef = this.modalService.show(EditTagComponent, initialState);
  }

  openDeleteTagModal() {
    const initialState: ModalOptions = {
      initialState: {
        name: this.tag?.name
      }
    }
    this.bsModalRef = this.modalService.show(DeleteConfirmationModalComponent, initialState);

    this.bsModalRef.content.deleteConfirmed.subscribe((deleteConfirmed: boolean) => {
      if (deleteConfirmed) {
        this.tagService.deleteTag(this.tag?.id!).subscribe({
          next: (response: any) => {
            this.messageService.add({ severity: 'success', summary: 'Success', detail: response.message, life: 3000 });
            this.tagService.removeTag(this.tag!.id)
          },
          error: (err: HttpErrorResponse) => {
            this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Problem with deleting task', life: 3000 });
          }
        })
      }
    });
  }
}
