import { Component, OnInit } from '@angular/core';
import { CategoryService } from '../category/category.service';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { switchMap } from 'rxjs/operators';
import { TaskService } from '../task/task.service';
import { GetTasksQueryParams } from '../shared/models/task/GetTasksQueryParams';
import { GroupingOptionsEnum } from '../shared/models/task/GroupingOptionsEnum';
import { SortingOptionsEnum } from '../shared/models/task/SortingOptionsEnum';
import { BsModalRef, BsModalService, ModalOptions } from 'ngx-bootstrap/modal';
import { EditCategoryComponent } from '../category/edit-category/edit-category.component';
import { DeleteConfirmationModalComponent } from '../shared/components/delete-confirmation-modal/delete-confirmation-modal.component';
import { MessageService } from 'primeng/api';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {
  protected categoryId!: number;
  protected contentLoaded: boolean = false;
  public groupOptions = GroupingOptionsEnum;
  public sortOptions = SortingOptionsEnum;
  bsModalRef?: BsModalRef;

  constructor(protected categoryService: CategoryService,
    protected activatedRoute: ActivatedRoute,
    protected taskService: TaskService,
    protected modalService: BsModalService,
    protected router: Router,
    protected messageService: MessageService) { }

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
        this.categoryId = id;
        this.taskService.QueryParams().categoryId = this.categoryId;
        this.getTasks();
        return this.categoryService.getCategory(+id);
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
      this.taskService.getTasks(true)?.subscribe(
        {
          error: error => console.log(error)
        }
      );
    }
  }

  onSortingOptionSelected(id: number) {
    const option = Object.values(SortingOptionsEnum).find(enumItem => enumItem.id === id);
    if (option !== this.taskService.QueryParams().sortBy) {
      this.taskService.QueryParams().sortBy = option!;
      this.taskService.QueryParams().categoryId = this.categoryService.selectedCategory()!.id;
      this.taskService.getTasks(true)?.subscribe(
        {
          error: error => console.log(error)
        }
      );
    }
  }

  onSortDirectionSelected(bool: boolean) {
    if (this.taskService.QueryParams().sortDescending !== bool) {
      this.taskService.QueryParams().sortDescending = bool;
      this.taskService.getTasks(true)?.subscribe(
        {
          error: error => console.log(error)
        }
      );
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
}

