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
  UserTasks = signal<Map<number, { [key: string]: TaskDto[] }>>(new Map<number, { [key: string]: TaskDto[] }>())
  CurrentCategoryTasksGroupNames = signal<string[] | undefined>([]);
  CurrentCategoryTasksMap = signal<{ [key: string]: TaskDto[] } | undefined>(undefined);
  QueryParams = signal<GetTasksQueryParams>(new GetTasksQueryParams);

  constructor(private http: HttpClient, private categoryService: CategoryService) { }

  addTask(task: TaskDto) {
    if (this.UserTasks().has(task.categoryId)) {
      this.UserTasks.mutate((val) => {
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

  getTasks(paramsChanged = false) {
    if (paramsChanged) {
      this.UserTasks().delete(this.QueryParams().categoryId)
    }

    if (this.UserTasks().has(this.QueryParams().categoryId)) {
      this.CurrentCategoryTasksMap.set(this.UserTasks().get(this.QueryParams().categoryId))
      this.CurrentCategoryTasksGroupNames.set(Object.keys(this.CurrentCategoryTasksMap()!))
      return;
    }

    let params = new HttpParams();

    if (this.QueryParams().categoryId > 0) params = params.append('categoryId', this.QueryParams().categoryId.toString());
    params = params.append('sortDescending', this.QueryParams().sortDescending.toString());
    params = params.append('sortBy', this.QueryParams().sortBy.apiName);
    if (this.QueryParams().groupBy) params = params.append('groupBy', this.QueryParams().groupBy.apiName);
    if (this.QueryParams().search) params = params.append('search', this.QueryParams().search);
    if (this.QueryParams().date) params = params.append('date', this.QueryParams().date)

    return this.http.get<ApiResponse<{ [key: string]: TaskDto[] }>>(this.apiUrl + 'Task', { params }).pipe(
      map(response => {
        this.UserTasks.mutate((val) => {
          val.set(this.QueryParams().categoryId, response.value)
        })
        this.CurrentCategoryTasksMap.set(this.UserTasks().get(this.QueryParams().categoryId))
        this.CurrentCategoryTasksGroupNames.set(Object.keys(this.CurrentCategoryTasksMap()!))
      })
    );
  }

  postTask(values: CreateTaskDto) {
    return this.http.post<ApiResponse<number>>(this.apiUrl + 'Task', values)
  }
}
