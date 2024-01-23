import { Component, OnInit } from '@angular/core';
import { CategoryService } from 'src/app/category/category.service';
import { DashboardComponent } from '../dashboard.component';
import { ActivatedRoute, Router } from '@angular/router';
import { NextActionsId } from 'src/app/shared/models/category/CategoryDto';
import { TaskService } from 'src/app/task/task.service';
import { BsModalService } from 'ngx-bootstrap/modal';
import { MessageService } from 'primeng/api';
import { AccountService } from 'src/app/account/account.service';
import { GroupingOptionsEnum } from 'src/app/shared/models/task/GroupingOptionsEnum';
import { SortingOptionsEnum } from 'src/app/shared/models/task/SortingOptionsEnum';
import { UserStoreService } from 'src/app/account/user-store.service';

@Component({
  selector: 'app-next-actions',
  template: '<app-dashboard></app-dashboard>'
})
export class NextActionsComponent extends DashboardComponent implements OnInit {
  constructor(
    categoryService: CategoryService,
    activatedRoute: ActivatedRoute,
    taskService: TaskService,
    modalService: BsModalService,
    router: Router,
    accountService: AccountService,
    messageService: MessageService,
    userStoreService: UserStoreService) {
    super(categoryService, activatedRoute,
      taskService, modalService, router, messageService, accountService, userStoreService);
  }


  override ngOnInit(): void {
    this.categoryService.getCategory(+NextActionsId).subscribe({
      error: error => console.log(error)
    })
    const userSettings = this.accountService.getUserSettings();
    const grouping = Object.values(GroupingOptionsEnum).find(enumItem => enumItem.apiName === userSettings?.nextActionsSettings.grouping);
    const sorting = Object.values(SortingOptionsEnum).find(enumItem => enumItem.apiName === userSettings?.nextActionsSettings.sorting);
    this.taskService.QueryParams().categoryId = NextActionsId; 
  
    this.taskService.QueryParams().groupBy = grouping!;
    this.taskService.QueryParams().sortBy = sorting!;
    this.taskService.QueryParams().sortDescending = userSettings?.nextActionsSettings.direction!;
    this.getTasks();
  }

}
