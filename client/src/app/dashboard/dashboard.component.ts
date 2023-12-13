import { Component, Input, OnInit, effect } from '@angular/core';
import { CategoryService } from '../category/category.service';
import { ActivatedRoute, Params } from '@angular/router';
import { CategoryDto } from '../shared/models/category/CategoryDto';
import { finalize, map, switchMap } from 'rxjs/operators';
import { TaskService } from '../task/task.service';
import { TaskDto } from '../shared/models/task/TaskDto';
import { GetTasksQueryParams } from '../shared/models/task/GetTasksQueryParams';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {
  protected categoryId!: number;
  groupedTasks = new Map<string, TaskDto[]>();
  queryParams = new GetTasksQueryParams;
  yourData: { [key: string]: TaskDto[] } = {};
  groupsNames: string[] = []
  protected contentLoaded: boolean = false;

  constructor(protected categoryService: CategoryService,
    protected activatedRoute: ActivatedRoute,
    protected taskService: TaskService) { }

  ngOnInit(): void {
    this.getCategory();
    this.contentLoaded = true;
  }

  private getCategory() {
    this.activatedRoute.params.pipe(
      switchMap((params: Params) => {
        const id = params['id'];
        if (!id) {
          return [];
        }
        this.categoryId = id;
        this.queryParams.categoryId = this.categoryId;
        this.getTasks();
        return this.categoryService.getCategory(+id);
      })
    ).subscribe({
      error: error => console.log(error)
    });
  }
  protected getTasks() {
    this.taskService.getTasks(this.queryParams)?.subscribe(
      {
        error: error => console.log(error)
      }
    );
  }

}

