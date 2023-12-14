import { Component, OnInit } from '@angular/core';
import { CategoryService } from '../category/category.service';
import { ActivatedRoute, Params } from '@angular/router';
import { switchMap } from 'rxjs/operators';
import { TaskService } from '../task/task.service';
import { TaskDto } from '../shared/models/task/TaskDto';
import { GetTasksQueryParams } from '../shared/models/task/GetTasksQueryParams';
import { MenuItem } from 'primeng/api';

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
  items: MenuItem[] | undefined;

  constructor(protected categoryService: CategoryService,
    protected activatedRoute: ActivatedRoute,
    protected taskService: TaskService) { }

  ngOnInit(): void {
    this.getCategory();
    this.items = [
      {
          label: 'File',
          icon: 'pi pi-file',
          items: [
              {
                  label: 'New',
                  icon: 'pi pi-plus',
                  items: [
                      {
                          label: 'Document',
                          icon: 'pi pi-file'
                      },
                      {
                          label: 'Image',
                          icon: 'pi pi-image'
                      },
                      {
                          label: 'Video',
                          icon: 'pi pi-video'
                      }
                  ]
              },
              {
                  label: 'Open',
                  icon: 'pi pi-folder-open'
              },
              {
                  label: 'Print',
                  icon: 'pi pi-print'
              }
          ]
      },
      {
          label: 'Edit',
          icon: 'pi pi-file-edit',
          items: [
              {
                  label: 'Copy',
                  icon: 'pi pi-copy'
              },
              {
                  label: 'Delete',
                  icon: 'pi pi-times'
              }
          ]
      },
      {
          label: 'Search',
          icon: 'pi pi-search'
      },
      {
          separator: true
      },
      {
          label: 'Share',
          icon: 'pi pi-share-alt',
          items: [
              {
                  label: 'Slack',
                  icon: 'pi pi-slack'
              },
              {
                  label: 'Whatsapp',
                  icon: 'pi pi-whatsapp'
              }
          ]
      }
  ]
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

