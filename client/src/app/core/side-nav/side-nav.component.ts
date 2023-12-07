import { Component, Input, OnInit } from '@angular/core';
import { CategoryService } from 'src/app/category/category.service';
import { CategoryDto } from 'src/app/shared/models/category/CategoryDto';
import { TaskService } from 'src/app/task/task.service';

@Component({
  selector: 'app-side-nav',
  templateUrl: './side-nav.component.html',
  styleUrls: ['./side-nav.component.scss']
})
export class SideNavComponent implements OnInit {
  userCategories: CategoryDto[] = [];

  constructor(public categoryService: CategoryService, private taskService: TaskService) { }

  ngOnInit(): void {
    this.getCategories();
  }

  selectCategory(id: number) {
    this.categoryService.selecetCategory(id);
  }

  getCategories() {
    this.categoryService.getCategories().subscribe({
      error: error => console.log(error)
    })
  }

}
