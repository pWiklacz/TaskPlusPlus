import { Component, OnInit } from '@angular/core';
import { CategoryService } from 'src/app/category/category.service';
import { DashboardComponent } from '../dashboard.component';
import { ActivatedRoute } from '@angular/router';
import { NextActionsId } from 'src/app/shared/models/CategoryDto';

@Component({
  selector: 'app-next-actions',
  template: '<app-dashboard></app-dashboard>'
})
export class NextActionsComponent extends DashboardComponent implements OnInit {
  constructor(
    categoryService: CategoryService,
    activatedRoute: ActivatedRoute
  ) {
    super(categoryService, activatedRoute);
  }

  override ngOnInit(): void {
    this.categoryService.getCategory(+NextActionsId).subscribe({
      error: error => console.log(error)
    })
  }
}
