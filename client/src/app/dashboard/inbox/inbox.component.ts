import { Component, OnInit } from '@angular/core';
import { CategoryService } from 'src/app/category/category.service';
import { DashboardComponent } from '../dashboard.component';
import { ActivatedRoute } from '@angular/router';
import { InboxId } from 'src/app/shared/models/CategoryDto';


@Component({
  selector: 'app-inbox',
  template: '<app-dashboard></app-dashboard>'
})
export class InboxComponent extends DashboardComponent implements OnInit {
  constructor(
    categoryService: CategoryService,
    activatedRoute: ActivatedRoute
  ) {
    super(categoryService, activatedRoute);
  }

  override ngOnInit(): void {
    this.categoryService.getCategory(+InboxId).subscribe({
      error: error => console.log(error)
    })
  }
}
