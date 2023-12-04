import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { AccountService } from 'src/app/account/account.service';
import { BsModalService, BsModalRef, ModalOptions } from 'ngx-bootstrap/modal';
import { SettingsComponent } from 'src/app/settings/settings.component';
import { AddCategoryComponent } from 'src/app/category/add-category/add-category.component';
import { SideNavService } from '../services/side-nav.service';
import { AddTaskComponent } from 'src/app/task/add-task/add-task.component';

@Component({
  selector: 'app-nav-bar-logged',
  templateUrl: './nav-bar-logged.component.html',
  styleUrls: ['./nav-bar-logged.component.scss']
})
export class NavBarLoggedComponent implements OnInit{
  bsModalRef?: BsModalRef;

  constructor(public accountService: AccountService, private modalService: BsModalService, private sideNavService: SideNavService) { }
  ngOnInit(): void {
    this.openAddTaskModal()
  }

  openSettingsModal() {
    this.bsModalRef = this.modalService.show(SettingsComponent, { backdrop: 'static', class: 'modal-dialog-centered' });
  }

  openAddCategoryModal() {
    this.bsModalRef = this.modalService.show(AddCategoryComponent, { backdrop: 'static', class: 'modal-dialog-centered' });

  }

  openAddTaskModal() {
    this.bsModalRef = this.modalService.show(AddTaskComponent, { backdrop: 'static', class: 'modal-dialog-centered' });
  }

  SideNavToggle() {
    this.sideNavService.updateSideNavStatus();
  }
}
