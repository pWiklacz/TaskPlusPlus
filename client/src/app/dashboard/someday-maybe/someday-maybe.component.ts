import { Component, OnInit } from '@angular/core';
import { CategoryService } from 'src/app/category/category.service';
import { DashboardComponent } from '../dashboard.component';
import { ActivatedRoute } from '@angular/router';
import { SomedayMaybeId } from 'src/app/shared/models/CategoryDto';

@Component({
  selector: 'app-someday-maybe',
  template: '<app-dashboard></app-dashboard>'
})
export class SomedayMaybeComponent extends DashboardComponent implements OnInit {
  constructor(
    categoryService: CategoryService,
    activatedRoute: ActivatedRoute
  ) {
    super(categoryService, activatedRoute);
  }

  override ngOnInit(): void {
    this.categoryService.getCategory(+SomedayMaybeId).subscribe({
      error: error => console.log(error)
    })
  }
}
