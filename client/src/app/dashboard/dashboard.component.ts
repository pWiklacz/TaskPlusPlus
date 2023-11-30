import { Component, OnInit, effect } from '@angular/core';
import { CategoryService } from '../category/category.service';
import { ActivatedRoute, Params } from '@angular/router';
import { CategoryDto } from '../shared/models/CategoryDto';
import { switchMap } from 'rxjs/operators';
import { TaskService } from '../task/task.service';
import { TaskDto } from '../shared/models/TaskDto';
import { GetTasksQueryParams } from '../shared/models/GetTasksQueryParams';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {
  protected categoryId?: number;
  groupedTasks = new Map<string, TaskDto[]>();
  queryParams = new GetTasksQueryParams;
  yourData: { [key: string]: TaskDto[] } = {};
  groupsNames: string[] = []

  constructor(protected categoryService: CategoryService,
    protected activatedRoute: ActivatedRoute,
    protected taskService: TaskService) { }

  ngOnInit(): void {
    this.getCategory() 
    this.queryParams.categoryId = this.categoryId!;
    if(this.queryParams.categoryId > 0)
    this.getTasks();
  }

  private getCategory() {
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

  protected getTasks() {
    this.taskService.getTasks(this.queryParams).subscribe({
      next: response => {
        this.yourData = response.value;
        console.log(this.yourData)
        this.groupsNames = Object.keys(this.yourData);
        const taask : TaskDto =this.yourData[this.groupsNames[0]][1];
        console.log(taask)
      }
    });
  }

}

