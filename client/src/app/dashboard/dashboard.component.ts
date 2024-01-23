import { Component, OnInit } from '@angular/core';
import { CategoryService } from '../category/category.service';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { map, switchMap } from 'rxjs/operators';
import { TaskService } from '../task/task.service';
import { GetTasksQueryParams } from '../shared/models/task/GetTasksQueryParams';
import { GroupingOptionsEnum } from '../shared/models/task/GroupingOptionsEnum';
import { SortingOptionsEnum } from '../shared/models/task/SortingOptionsEnum';
import { BsModalRef, BsModalService, ModalOptions } from 'ngx-bootstrap/modal';
import { EditCategoryComponent } from '../category/edit-category/edit-category.component';
import { DeleteConfirmationModalComponent } from '../shared/components/delete-confirmation-modal/delete-confirmation-modal.component';
import { MessageService } from 'primeng/api';
import { HttpErrorResponse } from '@angular/common/http';
import { AccountService } from '../account/account.service';
import { UpdateUserSettingsDto } from '../shared/models/account/UpdateUserSettingsDto';
import { UserStoreService } from '../account/user-store.service';
import { UserSettings, UserSettingsEnum } from '../shared/models/account/user';
import { CategorySettings } from '../shared/models/category/CategoryDto';
import { UpdateCategorySettingsDto } from '../shared/models/category/UpdateCategorySettingsDto';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {
  protected categoryId!: number;
  protected contentLoaded: boolean = false;
  protected groupOptions = GroupingOptionsEnum;
  protected sortOptions = SortingOptionsEnum;
  bsModalRef?: BsModalRef;

  constructor(protected categoryService: CategoryService,
    protected activatedRoute: ActivatedRoute,
    protected taskService: TaskService,
    protected modalService: BsModalService,
    protected router: Router,
    protected messageService: MessageService,
    protected accountService: AccountService,
    protected userStoreService: UserStoreService) { }

  ngOnInit(): void {
    this.getCategory();
    this.contentLoaded = true;
  }

  private getCategory() {
    this.activatedRoute.params.pipe(
      switchMap((params: Params) => {
        const id = params['id'];
        if (!id) {
          return [];
        }
        let category = this.categoryService.getCategory(+id).pipe(
          map(response => {
            this.categoryId = id;
            const categorySettings = this.categoryService.selectedCategory()?.settings;
            const grouping = Object.values(GroupingOptionsEnum).find(enumItem => enumItem.apiName === categorySettings?.grouping);
            const sorting = Object.values(SortingOptionsEnum).find(enumItem => enumItem.apiName === categorySettings?.sorting);
            this.taskService.QueryParams().categoryId = +this.categoryId;
            this.taskService.QueryParams().groupBy = grouping!;
            this.taskService.QueryParams().sortBy = sorting!;
            this.taskService.QueryParams().sortDescending = categorySettings?.direction!;
            this.getTasks();      
          })
        )    
        return category
      })
    ).subscribe({
      error: error => console.log(error)
    });
  }

  protected getTasks() {
    this.taskService.getTasks()?.subscribe(
      {
        error: error => console.log(error)
      }
    );
  }

  onGroupingOptionSelected(id: number) {
    const option = Object.values(GroupingOptionsEnum).find(enumItem => enumItem.id === id);
    if (option !== this.taskService.QueryParams().groupBy) {
      this.taskService.QueryParams().groupBy = option!;
      this.taskService.QueryParams().categoryId = this.categoryService.selectedCategory()!.id;
      if (this.categoryService.selectedCategory()?.isImmutable) {
        this.UpdateSystemCategoryGroupingOption()
      } else {
        this.UpdateUserCategoryGroupingOption(option?.apiName!);     
      }
    }
  }

  onSortingOptionSelected(id: number) {
    const option = Object.values(SortingOptionsEnum).find(enumItem => enumItem.id === id);
    if (option !== this.taskService.QueryParams().sortBy) {
      this.taskService.QueryParams().sortBy = option!;
      this.taskService.QueryParams().categoryId = this.categoryService.selectedCategory()!.id;
      if (this.categoryService.selectedCategory()?.isImmutable) {
        this.UpdateSystemCategorySortingOption()
      } else {
        this.UpdateUserCategorySortingOption(option?.apiName!)
      }
    }
  }

  onSortDirectionSelected(bool: boolean) {
    if (this.taskService.QueryParams().sortDescending !== bool) {
      this.taskService.QueryParams().sortDescending = bool;
      if (this.categoryService.selectedCategory()?.isImmutable) {
        this.UpdateSystemCategoryDirectionOption()
      } else {
       this.UpdateUserCategoryDirectionOption(this.taskService.QueryParams().sortDescending)
      }
    }
  }

  openEditCategoryModal() {
    const initialState: ModalOptions = {
      initialState: {
        category: this.categoryService.selectedCategory()
      },
      backdrop: 'static',
      class: 'modal-dialog-centered'
    }
    this.bsModalRef = this.modalService.show(EditCategoryComponent, initialState);
  }

  openDeleteModal() {
    const initialState: ModalOptions = {
      initialState: {
        name: this.categoryService.selectedCategory()?.name
      }
    }
    this.bsModalRef = this.modalService.show(DeleteConfirmationModalComponent, initialState);

    this.bsModalRef.content.deleteConfirmed.subscribe((deleteConfirmed: boolean) => {
      if (deleteConfirmed) {
        this.categoryService.deleteCategory(this.categoryService.selectedCategory()?.id!).subscribe({
          next: (response: any) => {
            this.messageService.add({ severity: 'success', summary: 'Success', detail: response.message, life: 3000 });
            this.router.navigate(['/dashboard/inbox']);
            this.categoryService.removeCategory(this.categoryService.selectedCategory()?.id!)
          },
          error: (err: HttpErrorResponse) => {
            this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Problem with deleting category', life: 3000 });
          }
        })
      }
    });
  }

  private UpdateSystemCategorySortingOption() {
    let userSettings = this.accountService.getUserSettings();
    const settingName = Object.values(UserSettingsEnum).find(
      enumItem => enumItem.categoryName === this.categoryService.selectedCategory()?.name)?.settingsName;

    switch (settingName) {
      case 'nextActionsSettings':
        userSettings?.nextActionsSettings && (
          userSettings.nextActionsSettings.sorting = this.taskService.QueryParams().sortBy?.apiName!);
        break;
      case 'inboxSettings':
        userSettings?.inboxSettings && (
          userSettings.inboxSettings.sorting = this.taskService.QueryParams().sortBy?.apiName!);
        break;
      case 'somedaySettings':
        userSettings?.somedaySettings && (
          userSettings.somedaySettings.sorting = this.taskService.QueryParams().sortBy?.apiName!);
        break;
      case 'todaySettings':
        userSettings?.todaySettings && (
          userSettings.todaySettings.sorting = this.taskService.QueryParams().sortBy?.apiName!);
        break;
      case 'waitngForSettings':
        userSettings?.waitingForSettings && (
          userSettings.waitingForSettings.sorting = this.taskService.QueryParams().sortBy?.apiName!);
        break;
      default:
        console.warn('Unsupported setting name:', settingName);
    }
    this.UpdateUserSettings(userSettings);
  }

  private UpdateSystemCategoryGroupingOption() {
    let userSettings = this.accountService.getUserSettings();
    const settingName = Object.values(UserSettingsEnum).find(
      enumItem => enumItem.categoryName === this.categoryService.selectedCategory()?.name)?.settingsName;

    switch (settingName) {
      case 'nextActionsSettings':
        userSettings?.nextActionsSettings && (
          userSettings.nextActionsSettings.grouping = this.taskService.QueryParams().groupBy?.apiName!);
        break;
      case 'inboxSettings':
        userSettings?.inboxSettings && (
          userSettings.inboxSettings.grouping = this.taskService.QueryParams().groupBy?.apiName!);
        break;
      case 'somedaySettings':
        userSettings?.somedaySettings && (
          userSettings.somedaySettings.grouping = this.taskService.QueryParams().groupBy?.apiName!);
        break;
      case 'todaySettings':
        userSettings?.todaySettings && (
          userSettings.todaySettings.grouping = this.taskService.QueryParams().groupBy?.apiName!);
        break;
      case 'waitngForSettings':
        userSettings?.waitingForSettings && (
          userSettings.waitingForSettings.grouping = this.taskService.QueryParams().groupBy?.apiName!);
        break;
      default:
        console.warn('Unsupported setting name:', settingName);
    }
    this.UpdateUserSettings(userSettings);
  }

  private UpdateSystemCategoryDirectionOption() {
    let userSettings = this.accountService.getUserSettings();
    const settingName = Object.values(UserSettingsEnum).find(
      enumItem => enumItem.categoryName === this.categoryService.selectedCategory()?.name)?.settingsName;

    switch (settingName) {
      case 'nextActionsSettings':
        userSettings?.nextActionsSettings && (
          userSettings.nextActionsSettings.direction = this.taskService.QueryParams().sortDescending);
        break;
      case 'inboxSettings':
        userSettings?.inboxSettings && (
          userSettings.inboxSettings.direction = this.taskService.QueryParams().sortDescending);
        break;
      case 'somedaySettings':
        userSettings?.somedaySettings && (
          userSettings.somedaySettings.direction = this.taskService.QueryParams().sortDescending);
        break;
      case 'todaySettings':
        userSettings?.todaySettings && (
          userSettings.todaySettings.direction = this.taskService.QueryParams().sortDescending);
        break;
      case 'waitngForSettings':
        userSettings?.waitingForSettings && (
          userSettings.waitingForSettings.direction = this.taskService.QueryParams().sortDescending);
        break;
      default:
        console.warn('Unsupported setting name:', settingName);
    }
    this.UpdateUserSettings(userSettings);
  }

  private UpdateUserSettings(userSettings: UserSettings | null) {
    const userSettingsDto: UpdateUserSettingsDto = {
      UserId: this.userStoreService.uid(),
      settings: userSettings!
    };
    this.accountService.updateSettings(userSettingsDto).subscribe({
      next: () => {
        const userSettingsJson = JSON.stringify(userSettings);
        localStorage.setItem('settings', userSettingsJson);
        this.taskService.getTasks(true)?.subscribe(
          {
            error: error => console.log(error)
          }
        );
      },
      error: (err: HttpErrorResponse) => {
        console.log(err);
      }
    });
  }

  private UpdateUserCategoryGroupingOption(option: string) {
    let category = this.categoryService.selectedCategory()!;
    category.settings.grouping = option;
    const UpdateSettingsDto: UpdateCategorySettingsDto = {
      id: this.categoryService.selectedCategory()!.id,
      settings: this.categoryService.selectedCategory()!.settings
    };
    this.categoryService.updateCategorySettings(UpdateSettingsDto).subscribe({
      next: () => {
        this.categoryService.updateCategory(category);

        this.taskService.getTasks(true)?.subscribe(
          {
            error: error => console.log(error)
          }
        );
      },
      error: (err: HttpErrorResponse) => {
        console.log(err);
      }
    });
  }

  private UpdateUserCategorySortingOption(option: string) {
    let category = this.categoryService.selectedCategory()!;
    category.settings.sorting = option;
    const UpdateSettingsDto: UpdateCategorySettingsDto = {
      id: this.categoryService.selectedCategory()!.id,
      settings: this.categoryService.selectedCategory()!.settings
    };
    this.categoryService.updateCategorySettings(UpdateSettingsDto).subscribe({
      next: () => {
        this.categoryService.updateCategory(category);

        this.taskService.getTasks(true)?.subscribe(
          {
            error: error => console.log(error)
          }
        );
      },
      error: (err: HttpErrorResponse) => {
        console.log(err);
      }
    });
  }

  private UpdateUserCategoryDirectionOption(option: boolean) {
    let category = this.categoryService.selectedCategory()!;
    category.settings.direction = option;
    const UpdateSettingsDto: UpdateCategorySettingsDto = {
      id: this.categoryService.selectedCategory()!.id,
      settings: this.categoryService.selectedCategory()!.settings
    };
    this.categoryService.updateCategorySettings(UpdateSettingsDto).subscribe({
      next: () => {
        this.categoryService.updateCategory(category);

        this.taskService.getTasks(true)?.subscribe(
          {
            error: error => console.log(error)
          }
        );
      },
      error: (err: HttpErrorResponse) => {
        console.log(err);
      }
    });
  }
}
