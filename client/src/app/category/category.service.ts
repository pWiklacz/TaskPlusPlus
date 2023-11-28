import { HttpClient } from '@angular/common/http';
import { Injectable, signal } from '@angular/core';
import { environment } from 'src/environments/environment';
import { CategoryDto, CreateCategoryDto } from '../shared/models/CategoryDto';
import { ApiResponse } from '../shared/models/ApiResponse';
import { Observable, map } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {
  apiUrl = environment.apiUrl;
  public userCategories = signal<CategoryDto[]>([]);
  public selectedCategory = signal<CategoryDto | undefined>(undefined);
 
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

  constructor(private http: HttpClient) { }

  getCategories() {
    return this.http.get<ApiResponse<CategoryDto[]>>(this.apiUrl + 'Category').pipe(
      map(categories => {
        this.userCategories.set(categories.value)
      }
      ));
  }

  selecetCategory(id: number) {
    this.selectedCategory.update(() => this.userCategories()[id])
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
