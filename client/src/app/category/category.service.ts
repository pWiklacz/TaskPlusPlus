import { HttpClient } from '@angular/common/http';
import { Injectable, signal } from '@angular/core';
import { environment } from 'src/environments/environment';
import { CategoryDto } from '../shared/models/category/CategoryDto';
import { ApiResponse } from '../shared/models/ApiResponse';
import { map } from 'rxjs';
import { CreateCategoryDto } from '../shared/models/category/CreateCategoryDto';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {
  apiUrl = environment.apiUrl;
  public userCategories = signal<CategoryDto[]>([]);
  public selectedCategory = signal<CategoryDto | undefined>(undefined);
  public systemCategories = [
    {
      id: '1',
      name: 'Inbox',
      icon: 'fa-solid fa-inbox',
      color: '#4cd6f1'
    },
    {
      id: '2',
      name: 'Today',
      icon: 'fa-solid fa-calendar-day',
      color: '#065535'
    },
    {
      id: '3',
      name: 'Calendar',
      icon: 'fa-solid fa-calendar',
      color: '#cb063e'
    },
    {
      id: '4',
      name: 'Next Actions',
      icon: 'fa-solid fa-angles-right',
      color: '#f37b16'
    },
    {
      id: '5',
      name: 'Projects',
      icon: 'fa-solid fa-list-check',
      color: '#ffffff'
    },
    {
      id: '6',
      name: 'Waiting For',
      icon: 'fa-solid fa-hourglass-half',
      color: '#000000'
    },
    {
      id: '7',
      name: 'Someday/Maybe',
      icon: 'fa-solid fa-lightbulb',
      color: '#f3f316'
    }
  ];

  constructor(private http: HttpClient) { }

  addCategory(category: CategoryDto) {
    this.userCategories.mutate((val) => {
      val.push(category)
    })
  }

  removeCategory(id: number) {
    this.userCategories.mutate((val) => {
      val.splice(id, 1);
    })
  }

  selecetCategory(id: number) {
    this.selectedCategory.update(() => this.userCategories()[id])
  }

  getCategories() {
    return this.http.get<ApiResponse<CategoryDto[]>>(this.apiUrl + 'Category').pipe(
      map(categories => {
        this.userCategories.set(categories.value)
      }
      ));
  }

  postCategory(values: CreateCategoryDto) {
    return this.http.post<ApiResponse<number>>(this.apiUrl + 'Category', values);
  }

  getCategory(id: number) {
    return this.http.get<ApiResponse<CategoryDto>>(this.apiUrl + 'Category/' + id).pipe(
      map(category => {
        this.selectedCategory.update(() => category.value)
      })
    );
  }

  deleteCategory(id: number) {
    return this.http.delete(this.apiUrl + 'Category/' + id);
  }

  putCategory(updatedCategory: CategoryDto) {
    return this.http.put(this.apiUrl + 'Category/' + updatedCategory.id, updatedCategory);
  }
}
