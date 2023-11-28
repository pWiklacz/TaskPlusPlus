import { Component, OnInit, effect } from '@angular/core';
import { CategoryService } from '../category/category.service';
import { ActivatedRoute, Params } from '@angular/router';
import { CategoryDto } from '../shared/models/CategoryDto';
import { switchMap } from 'rxjs/operators';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {
  categoryId?: number;

  constructor(protected categoryService: CategoryService, protected activatedRoute: ActivatedRoute) {}

  ngOnInit(): void {
    this.activatedRoute.params.pipe(
      switchMap((params: Params) => {
        const id = params['id'];
        if (!id) {         
          return [];
        }
        this.categoryId = id;
        return this.categoryService.getCategory(+id);
      })
    ).subscribe({
      error: error => console.log(error)
    });  
  }
}

