import { HttpClient } from '@angular/common/http';
import { Injectable, signal } from '@angular/core';
import { environment } from 'src/environments/environment';
import { CategoryDto } from '../shared/models/category/CategoryDto';
import { ApiResponse } from '../shared/models/ApiResponse';
import { map } from 'rxjs';
import { CreateCategoryDto } from '../shared/models/category/CreateCategoryDto';
import { UpdateCategorySettingsDto } from '../shared/models/category/UpdateCategorySettingsDto';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {
  apiUrl = environment.apiUrl;
  public userCategories = signal<CategoryDto[]>([]);
  public selectedCategory = signal<CategoryDto | undefined>(undefined);
  
  public systemCategories : CategoryDto[] = [
    {
      id: 1,
      name: 'Inbox',
      icon: 'fa-solid fa-inbox',
      isFavorite: false,
      isImmutable: true,
      colorHex: '#4cd6f1',
      settings: {
        grouping: 'None',
        sorting: 'Name',
        direction: false,
      }
    },
    {
      id: 0,
      name: 'Today',
      icon: 'fa-solid fa-calendar-day',
      isFavorite: false,
      isImmutable: true,
      colorHex: '#065535',
      settings: {
        grouping: 'None',
        sorting: 'Name',
        direction: false,
      }
    },
    {
      id: 0,
      name: 'Calendar',
      icon: 'fa-solid fa-calendar',
      isFavorite: false,
      isImmutable: true,
      colorHex: '#cb063e',
      settings: {
        grouping: 'None',
        sorting: 'Name',
        direction: false,
      }
    },
    {
      id: 2,
      name: 'Next Actions',
      icon: 'fa-solid fa-angles-right',
      isFavorite: false,
      isImmutable: true,
      colorHex: '#f37b16',
      settings: {
        grouping: 'None',
        sorting: 'Name',
        direction: false
      }
    },
    {
      id: 0,
      name: 'Projects',
      icon: 'fa-solid fa-list-check',
      isFavorite: false,
      isImmutable: true,
      colorHex: '#ffffff',
      settings: {
        grouping: 'None',
        sorting: 'Name',
        direction: false
      }
    },
    {
      id: 5,
      name: 'Waiting For',
      icon: 'fa-solid fa-hourglass-half',
      isFavorite: false,
      isImmutable: true,
      colorHex: '#000000',
      settings: {
        grouping: 'None',
        sorting: 'Name',
        direction: false,
      }
    },
    {
      id: 6,
      name: 'Someday/Maybe',
      icon: 'fa-solid fa-lightbulb',
      isFavorite: false,
      isImmutable: true,
      colorHex: '#f3f316',
      settings: {
        grouping: 'None',
        sorting: 'Name',
        direction: false,
      }
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
      const index = val.findIndex(cat => cat.id == id)
      if (index !== -1) {
        val.splice(index, 1);
      }
    })
  }

  updateCategory(category: CategoryDto) {
    this.userCategories.mutate((val) => {
      const index = val.findIndex(cat => cat.id == category.id)
      if (index !== -1) {
        val[index] = category;
      }
    })
  }

  selectCategory(category: CategoryDto){
    this.selectedCategory.update(() => category)
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

  updateCategorySettings(settings: UpdateCategorySettingsDto){
    return this.http.put(this.apiUrl + 'Category/' + settings.id + '/updateSettings', settings);
  }
}
