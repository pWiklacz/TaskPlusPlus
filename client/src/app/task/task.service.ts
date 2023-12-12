import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable, signal } from '@angular/core';
import { environment } from 'src/environments/environment';
import { ApiResponse } from '../shared/models/ApiResponse';
import { TaskDto } from '../shared/models/task/TaskDto';
import { GetTasksQueryParams } from '../shared/models/task/GetTasksQueryParams';
import { CreateTaskDto } from '../shared/models/task/CreateTaskDto';
import { map } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class TaskService {
  apiUrl = environment.apiUrl;
  categoryTasks = signal<Map<number, { [key: string]: TaskDto[] }>>(new Map<number, { [key: string]: TaskDto[] }>())

  constructor(private http: HttpClient) { }

  getCategoryTaskGroupsNames(id: number) {
    let tasksMap = this.categoryTasks().get(id);
    if (tasksMap) return Object.keys(tasksMap);
    return
  }

  getCategoryGroupOfTasks(id: number, key: string) {
    let tasksMap = this.categoryTasks().get(id);
    return tasksMap![key];
  }

  addTask(task: TaskDto) {
     if (this.categoryTasks().has(task.categoryId)) {
      this.categoryTasks.mutate((val) => {
        let categoryTasks = val.get(task.categoryId)
        categoryTasks!['All'].push(task)
      })
    }
  }

  getTasks(queryParams: GetTasksQueryParams) {

    if (this.categoryTasks().has(queryParams.categoryId)) {
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
        this.categoryTasks.mutate((val) => {
          val.set(queryParams.categoryId, response.value)
        })
      })
    );
  }

  postTask(values: CreateTaskDto) {
    return this.http.post<ApiResponse<number>>(this.apiUrl + 'Task', values)
  }
}
