import { Component, OnInit } from '@angular/core';
import { ProjectService } from 'src/app/project/project.service';
import { TaskService } from 'src/app/task/task.service';

@Component({
  selector: 'app-projects',
  templateUrl: './projects.component.html',
  styleUrls: ['./projects.component.scss']
})
export class ProjectsComponent implements OnInit {
  constructor(public projectService: ProjectService) {
  }

  ngOnInit(): void {
    this.getProjects();
  }

  private getProjects() {
    this.projectService.getProjects()?.subscribe({
      error: error => console.log(error)
    });
  }
}
