import { Injectable, signal } from '@angular/core';
import { environment } from 'src/environments/environment';
import { ProjectDto } from '../shared/models/project/ProjectDto';
import { HttpClient } from '@angular/common/http';
import { ApiResponse } from '../shared/models/ApiResponse';
import { map } from 'rxjs';
import { CreateProjectDto } from '../shared/models/project/CreateProjectDto';
import { TaskDto } from '../shared/models/task/TaskDto';
import { EditProjectDto } from '../shared/models/project/EditProjectDto';

@Injectable({
  providedIn: 'root'
})
export class ProjectService {
  apiUrl = environment.apiUrl;
  UserProjects = signal<ProjectDto[]>([]);
  constructor(private http: HttpClient) { }

  addProject(project: ProjectDto) {
    this.UserProjects.mutate((val) => {
      val.push(project)
    })
  }

  removeProject(id: number) {
    this.UserProjects.mutate((val) => {
      const index = val.findIndex(p => p.id == id)
      if (index !== -1) {
        val.splice(index, 1);
      }
    })
  }

  updateProject(project: ProjectDto) {
    this.UserProjects.mutate((val) => {
      const index = val.findIndex(p => p.id == project.id)
      if (index !== -1) {
        val[index] = project;
      }
    })
  }

  addTaskToProject(id: number, task: TaskDto) {
    this.UserProjects.mutate((val) => {
      const index = val.findIndex(p => p.id == id)
      if (index !== -1) {
        val[index].tasks.push(task)
      }
    })
  }

  editTaskInProject(id: number, task: TaskDto) {
    this.UserProjects.mutate((val) => {
      const index = val.findIndex(p => p.id == id)
      if (index !== -1) {
        const projectTasks = val[index].tasks || [];
        const taskIndex = projectTasks.findIndex(t => t.id === task.id);
        if (taskIndex !== -1) {
          projectTasks[taskIndex] = task;
          val[index].tasks = [...projectTasks]
        }
      }
    })
  }

  removeTaskInProject(id: number, task: TaskDto) {
    this.UserProjects.mutate((val) => {
      const index = val.findIndex(p => p.id == id)
      if (index !== -1) {
        const projectTasks = val[index].tasks || [];
        const taskIndex = projectTasks.findIndex(t => t.id === task.id);
        if (taskIndex !== -1) {
          projectTasks.splice(taskIndex, 1)
          val[index].tasks = [...projectTasks]
        }
      }
    })
  }

  getProjects() {
    if (this.UserProjects().length == 0) {
      return this.http.get<ApiResponse<ProjectDto[]>>(this.apiUrl + 'Project').pipe(
        map(projects => {
          this.UserProjects.set(projects.value)
        })
      )
    }
    return
  }

  changeCompleteStatus(isComplete: boolean, id: number) {
    return this.http.put(this.apiUrl + 'Project/' + id + '/changeCompleteStatus', { id, isComplete })
  }
  postProject(values: CreateProjectDto) {
    return this.http.post<ApiResponse<number>>(this.apiUrl + 'Project', values);
  }

  deleteProject(id: number) {
    return this.http.delete(this.apiUrl + 'Project/' + id);
  }

  putProject(updatedProject: EditProjectDto) {
    return this.http.put(this.apiUrl + 'Project/' + updatedProject.id, updatedProject);
  }
}
