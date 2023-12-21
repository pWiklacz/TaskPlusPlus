import { Component, OnInit } from '@angular/core';
import { CategoryService } from 'src/app/category/category.service';
import { DashboardComponent } from '../dashboard.component';
import { ActivatedRoute, Router } from '@angular/router';
import { InboxId } from 'src/app/shared/models/category/CategoryDto';
import { TaskService } from 'src/app/task/task.service';
import { BsModalService } from 'ngx-bootstrap/modal';
import { MessageService } from 'primeng/api';


@Component({
  selector: 'app-inbox',
  templateUrl: './inbox.component.html',
  styleUrls: ['./inbox.component.scss']
})
export class InboxComponent extends DashboardComponent implements OnInit {

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
    this.categoryService.getCategory(+InboxId).subscribe({
      error: error => console.log(error)
    })
    this.taskService.QueryParams().categoryId = InboxId;
    this.getTasks();
  }
}
