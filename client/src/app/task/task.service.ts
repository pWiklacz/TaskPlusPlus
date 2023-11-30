import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { ApiResponse } from '../shared/models/ApiResponse';
import { TaskDto } from '../shared/models/TaskDto';
import { GetTasksQueryParams } from '../shared/models/GetTasksQueryParams';

@Injectable({
  providedIn: 'root'
})
export class TaskService {
  apiUrl = environment.apiUrl;
 
  constructor(private http: HttpClient) { }

  getTasks(queryParams: GetTasksQueryParams) {

    let params = new HttpParams();

    if (queryParams.categoryId > 0) params = params.append('categoryId', queryParams.categoryId.toString());
    params = params.append('sortDescending', queryParams.sortDescending.toString());
    params = params.append('sortBy', queryParams.sortBy);
    if(queryParams.groupBy) params = params.append('groupBy', queryParams.groupBy);
    if(queryParams.search) params = params.append('search', queryParams.search);

    return this.http.get<ApiResponse<{[key: string]: TaskDto[] }>>(this.apiUrl + 'Task', {params});
  }
}
