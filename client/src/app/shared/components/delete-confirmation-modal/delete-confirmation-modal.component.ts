import { Component } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { Subject } from 'rxjs';

@Component({
  selector: 'app-delete-confirmation-modal',
  templateUrl: './delete-confirmation-modal.component.html',
  styleUrls: ['./delete-confirmation-modal.component.scss']
})
export class DeleteConfirmationModalComponent {
  name?: string;
  public deleteConfirmed = new Subject<boolean>();
  
  constructor(public bsModalRef: BsModalRef) { }

  public onDelete() {
    this.deleteConfirmed.next(true);
    this.bsModalRef.hide();
  }
}
