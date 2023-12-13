import { Component, OnInit } from '@angular/core';
import { CategoryService } from 'src/app/category/category.service';
import { DashboardComponent } from '../dashboard.component';
import { ActivatedRoute } from '@angular/router';
import { NextActionsId } from 'src/app/shared/models/category/CategoryDto';
import { TaskService } from 'src/app/task/task.service';

@Component({
  selector: 'app-next-actions',
  template: '<app-dashboard></app-dashboard>'
})
export class NextActionsComponent extends DashboardComponent implements OnInit {
  constructor(
    categoryService: CategoryService,
    activatedRoute: ActivatedRoute,
    taskService: TaskService) 
  {
    super(categoryService, activatedRoute, taskService);
  }

  override ngOnInit(): void {
    this.categoryService.getCategory(+NextActionsId).subscribe({
      error: error => console.log(error)
    })
    this.queryParams.categoryId = NextActionsId;
    this.getTasks();
  }
}
