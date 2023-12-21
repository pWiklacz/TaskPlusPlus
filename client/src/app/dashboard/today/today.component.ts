import { Component, OnDestroy, OnInit } from '@angular/core';
import { CategoryService } from 'src/app/category/category.service';
import { DashboardComponent } from '../dashboard.component';
import { ActivatedRoute, Router } from '@angular/router';
import { TaskService } from 'src/app/task/task.service';
import { BsModalService } from 'ngx-bootstrap/modal';
import { MessageService } from 'primeng/api';

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
    messageService: MessageService) {
    super(categoryService, activatedRoute,
      taskService, modalService, router, messageService);
  }
  ngOnDestroy(): void {
    this.taskService.QueryParams().date = '';
  }

  override ngOnInit(): void {
    const today = this.categoryService.systemCategories.find(cat => cat.name == 'Today');
    this.categoryService.selectCategory(today!)
    let date = new Date();
    let formattedDate = `${date.getFullYear()}-${(date.getMonth() + 1).toString().padStart(2, '0')}-${date.getDate().toString().padStart(2, '0')}`;
    this.taskService.QueryParams().categoryId  = this.categoryService.selectedCategory()!.id;
    this.taskService.QueryParams().date = formattedDate;
    this.getTasks();
  }
}
