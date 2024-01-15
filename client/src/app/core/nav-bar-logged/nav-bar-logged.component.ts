import { AfterViewInit, Component, OnDestroy, OnInit } from '@angular/core';
import { AccountService } from 'src/app/account/account.service';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { SettingsComponent } from 'src/app/settings/settings.component';
import { AddCategoryComponent } from 'src/app/category/add-category/add-category.component';
import { SideNavService } from '../services/side-nav.service';
import { AddTaskComponent } from 'src/app/task/add-task/add-task.component';
import { AddTagComponent } from 'src/app/tag/add-tag/add-tag.component';
import { AddProjectComponent } from 'src/app/project/add-project/add-project.component';
import { SettingsService } from 'src/app/settings/settings.service';
import { NavigationStart, Router } from '@angular/router';
import { UserStoreService } from 'src/app/account/user-store.service';


@Component({
  selector: 'app-nav-bar-logged',
  templateUrl: './nav-bar-logged.component.html',
  styleUrls: ['./nav-bar-logged.component.scss']
})
export class NavBarLoggedComponent implements OnInit {
  bsModalRef?: BsModalRef;

  constructor(public accountService: AccountService,
    private modalService: BsModalService,
    private sideNavService: SideNavService,
    public settingsService: SettingsService,
    private router: Router,
    public userStoreService: UserStoreService) { }

  ngOnInit(): void {
    this.router.events.subscribe((event) => {
      if (event instanceof NavigationStart) {
        console.log(this.settingsService.isOpen)
        if (event.url.includes('settings') && !this.settingsService.isOpen) {
          this.openSettingsModal()
        }
      }
    });
  }

  openSettingsModal() {
    this.settingsService.setOpenState(true)
    this.bsModalRef = this.modalService.show(SettingsComponent, { backdrop: 'static', class: 'modal-dialog-centered modal-lg ' });
  }

  openAddCategoryModal() {
    this.bsModalRef = this.modalService.show(AddCategoryComponent, { backdrop: 'static', class: 'modal-dialog-centered' });
  }

  openAddTagModal() {
    this.bsModalRef = this.modalService.show(AddTagComponent, { backdrop: 'static', class: 'modal-dialog-centered' });
  }

  openAddTaskModal() {
    this.bsModalRef = this.modalService.show(AddTaskComponent, { backdrop: 'static', class: 'modal-dialog-centered' });
  }

  openAddProjectModal() {
    this.bsModalRef = this.modalService.show(AddProjectComponent, { backdrop: 'static', class: 'modal-dialog-centered' });
  }
  SideNavToggle() {
    this.sideNavService.updateSideNavStatus();
  }
}
