import { Component } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { Subject } from 'rxjs';

@Component({
  selector: 'app-add-task-time-conflict-modal',
  templateUrl: './add-task-time-conflict-modal.component.html',
  styleUrls: ['./add-task-time-conflict-modal.component.scss']
})
export class AddTaskTimeConflictModalComponent {
  
  public Confirmed = new Subject<boolean>();

  constructor(public bsModalRef: BsModalRef) { }

  public onConfirm() {
    this.bsModalRef.onHidden!.next('confirm');
    this.bsModalRef.hide();
  }
}
