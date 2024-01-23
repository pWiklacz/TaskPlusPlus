import { Component, OnDestroy, OnInit } from '@angular/core';
import { CategoryService } from 'src/app/category/category.service';
import { DashboardComponent } from '../dashboard.component';
import { ActivatedRoute, Router } from '@angular/router';
import { TaskService } from 'src/app/task/task.service';
import { BsModalService } from 'ngx-bootstrap/modal';
import { MessageService } from 'primeng/api';
import { AccountService } from 'src/app/account/account.service';
import { GroupingOptionsEnum } from 'src/app/shared/models/task/GroupingOptionsEnum';
import { SortingOptionsEnum } from 'src/app/shared/models/task/SortingOptionsEnum';
import { UserStoreService } from 'src/app/account/user-store.service';
import { UpdateUserSettingsDto } from 'src/app/shared/models/account/UpdateUserSettingsDto';
import { HttpErrorResponse } from '@angular/common/http';
import { UserSettings } from 'src/app/shared/models/account/user';

@Component({
  selector: 'app-today',
  templateUrl: './today.component.html',
  styleUrls: ['./today.component.scss']
})
export class TodayComponent extends DashboardComponent implements OnInit, OnDestroy {
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


  ngOnDestroy(): void {
    this.taskService.QueryParams().date = '';
  }

  override ngOnInit(): void {
    const today = this.categoryService.systemCategories.find(cat => cat.name == 'Today');
    this.categoryService.selectCategory(today!)
    let date = new Date();
    let formattedDate = `${date.getFullYear()}-${(date.getMonth() + 1).toString().padStart(2, '0')}-${date.getDate().toString().padStart(2, '0')}`;
    const userSettings = this.accountService.getUserSettings();
    const grouping = Object.values(GroupingOptionsEnum)
      .find(enumItem => enumItem.apiName === userSettings?.todaySettings.grouping);
    const sorting = Object.values(SortingOptionsEnum)
      .find(enumItem => enumItem.apiName === userSettings?.todaySettings.sorting);
    this.taskService.QueryParams().groupBy = grouping!;
    this.taskService.QueryParams().sortBy = sorting!;
    this.taskService.QueryParams().sortDescending = userSettings?.todaySettings.direction!;
    this.taskService.QueryParams().categoryId = this.categoryService.selectedCategory()!.id;
    this.taskService.QueryParams().date = formattedDate;
    this.getTasks();
  }

}
