import { Component, OnInit } from '@angular/core';
import { CategoryService } from 'src/app/category/category.service';
import { DashboardComponent } from '../dashboard.component';
import { ActivatedRoute } from '@angular/router';
import { InboxId } from 'src/app/shared/models/CategoryDto';
import { TaskService } from 'src/app/task/task.service';


@Component({
  selector: 'app-inbox',
  template: '<app-dashboard></app-dashboard>'
})
export class InboxComponent extends DashboardComponent implements OnInit {
 
  constructor(
    categoryService: CategoryService,
    activatedRoute: ActivatedRoute,
    taskService: TaskService) {
    super(categoryService, activatedRoute, taskService);
    
  }


  override ngOnInit(): void {
    this.categoryService.getCategory(+InboxId).subscribe({
      error: error => console.log(error)
    })
    this.queryParams.categoryId = InboxId;
    this.getTasks();
  }
}
