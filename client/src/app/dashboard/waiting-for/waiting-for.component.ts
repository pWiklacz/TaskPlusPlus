import { Component, OnInit } from '@angular/core';
import { CategoryService } from 'src/app/category/category.service';
import { DashboardComponent } from '../dashboard.component';
import { ActivatedRoute, Router } from '@angular/router';
import { WaitingForId } from 'src/app/shared/models/category/CategoryDto';
import { TaskService } from 'src/app/task/task.service';
import { BsModalService } from 'ngx-bootstrap/modal';
import { MessageService } from 'primeng/api';
import { GroupingOptionsEnum } from 'src/app/shared/models/task/GroupingOptionsEnum';

@Component({
  selector: 'app-waiting-for',
  template: '<app-dashboard></app-dashboard>'
})
export class WaitingForComponent extends DashboardComponent implements OnInit {
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
  override ngOnInit(): void {
    this.categoryService.getCategory(+WaitingForId).subscribe({
      error: error => console.log(error)
    })
    this.taskService.QueryParams().categoryId = WaitingForId;
    this.getTasks();
  }

 
}
