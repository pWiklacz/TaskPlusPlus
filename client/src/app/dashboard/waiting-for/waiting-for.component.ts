import { Component, OnInit } from '@angular/core';
import { CategoryService } from 'src/app/category/category.service';
import { DashboardComponent } from '../dashboard.component';
import { ActivatedRoute } from '@angular/router';
import { WaitingForId } from 'src/app/shared/models/CategoryDto';

@Component({
  selector: 'app-waiting-for',
  template: '<app-dashboard></app-dashboard>'
})
export class WaitingForComponent extends DashboardComponent implements OnInit {
  constructor(
    categoryService: CategoryService,
    activatedRoute: ActivatedRoute
  ) {
    super(categoryService, activatedRoute);
  }

  override ngOnInit(): void {
    this.categoryService.getCategory(+WaitingForId).subscribe({
      error: error => console.log(error)
    })
  }
}
