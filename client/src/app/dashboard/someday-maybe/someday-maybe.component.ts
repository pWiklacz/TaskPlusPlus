import { Component, OnInit } from '@angular/core';
import { CategoryService } from 'src/app/category/category.service';
import { DashboardComponent } from '../dashboard.component';
import { ActivatedRoute } from '@angular/router';
import { SomedayMaybeId } from 'src/app/shared/models/category/CategoryDto';
import { TaskService } from 'src/app/task/task.service';

@Component({
  selector: 'app-someday-maybe',
  template: '<app-dashboard></app-dashboard>'
})
export class SomedayMaybeComponent extends DashboardComponent implements OnInit {
  constructor(
    categoryService: CategoryService,
    activatedRoute: ActivatedRoute,
    taskService: TaskService) 
  {
    super(categoryService, activatedRoute, taskService);
  }


  override ngOnInit(): void {
    this.categoryService.getCategory(+SomedayMaybeId).subscribe({
      error: error => console.log(error)
    })
  }
}
