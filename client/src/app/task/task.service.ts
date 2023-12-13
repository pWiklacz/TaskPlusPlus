import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable, Signal, computed, signal } from '@angular/core';
import { environment } from 'src/environments/environment';
import { ApiResponse } from '../shared/models/ApiResponse';
import { TaskDto } from '../shared/models/task/TaskDto';
import { GetTasksQueryParams } from '../shared/models/task/GetTasksQueryParams';
import { CreateTaskDto } from '../shared/models/task/CreateTaskDto';
import { map } from 'rxjs';
import { CategoryService } from '../category/category.service';

@Injectable({
  providedIn: 'root'
})
export class TaskService {
  apiUrl = environment.apiUrl;
  userTasks = signal<Map<number, { [key: string]: TaskDto[] }>>(new Map<number, { [key: string]: TaskDto[] }>())
  CurrentCategoryTasksGroupNames = signal<string[] | undefined>([]);
  CurrentCategoryTasksMap = signal<{ [key: string]: TaskDto[] } | undefined>(undefined);

  constructor(private http: HttpClient, private categoryService: CategoryService) { }

  addTask(task: TaskDto) {
    if (this.userTasks().has(task.categoryId)) {
      this.userTasks.mutate((val) => {
        let categoryTasks = val.get(task.categoryId)
        categoryTasks!['All'].push(task)
      })
    }
    if (+this.categoryService.selectedCategory()!.id === +task.categoryId) {
      this.CurrentCategoryTasksMap.mutate((val) => {
        val!['All'].push(task)
      })
    }
  }

  getTasks(queryParams: GetTasksQueryParams) {
    if (this.userTasks().has(queryParams.categoryId)) {
      this.CurrentCategoryTasksMap.set(this.userTasks().get(queryParams.categoryId))
      this.CurrentCategoryTasksGroupNames.set(Object.keys(this.CurrentCategoryTasksMap()!))
      return;
    }

    let params = new HttpParams();

    if (queryParams.categoryId > 0) params = params.append('categoryId', queryParams.categoryId.toString());
    params = params.append('sortDescending', queryParams.sortDescending.toString());
    params = params.append('sortBy', queryParams.sortBy);
    if (queryParams.groupBy) params = params.append('groupBy', queryParams.groupBy);
    if (queryParams.search) params = params.append('search', queryParams.search);

    return this.http.get<ApiResponse<{ [key: string]: TaskDto[] }>>(this.apiUrl + 'Task', { params }).pipe(
      map(response => {
        this.userTasks.mutate((val) => {
          val.set(queryParams.categoryId, response.value)
        })
        this.CurrentCategoryTasksMap.set(this.userTasks().get(queryParams.categoryId))
        this.CurrentCategoryTasksGroupNames.set(Object.keys(this.CurrentCategoryTasksMap()!))
      })
    );
  }

  postTask(values: CreateTaskDto) {
    return this.http.post<ApiResponse<number>>(this.apiUrl + 'Task', values)
  }
}
