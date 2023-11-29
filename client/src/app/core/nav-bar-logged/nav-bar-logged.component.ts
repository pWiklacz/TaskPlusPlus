import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { AccountService } from 'src/app/account/account.service';
import { BsModalService, BsModalRef, ModalOptions } from 'ngx-bootstrap/modal';
import { SettingsComponent } from 'src/app/settings/settings.component';
import { AddCategoryComponent } from 'src/app/category/add-category/add-category.component';
import { SideNavService } from '../services/side-nav.service';

@Component({
  selector: 'app-nav-bar-logged',
  templateUrl: './nav-bar-logged.component.html',
  styleUrls: ['./nav-bar-logged.component.scss']
})
export class NavBarLoggedComponent implements OnInit{
  //@Output() sideNavToggled = new EventEmitter<boolean>();
  //menuStatus: boolean = true;

  bsModalRef?: BsModalRef;
  constructor(public accountService: AccountService, private modalService: BsModalService, private sideNavService: SideNavService) { }
  ngOnInit(): void {

  }

  openSettingsModal() {
    this.bsModalRef = this.modalService.show(SettingsComponent, { backdrop: 'static', class: 'modal-dialog-centered' });
    this.bsModalRef.content.closeBtnName = 'Close';
  }

  openAddCategoryModal() {
    this.bsModalRef = this.modalService.show(AddCategoryComponent, { backdrop: 'static', class: 'modal-dialog-centered' });
    this.bsModalRef.content.closeBtnName = 'Close';
  }

  SideNavToggle() {
    //this.menuStatus = !this.menuStatus;
    this.sideNavService.updateSideNavStatus();
  }
}
