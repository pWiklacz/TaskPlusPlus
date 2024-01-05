import { Component, Input, OnInit, signal } from '@angular/core';
import { ProjectService } from '../project.service';
import { ProjectDto } from 'src/app/shared/models/project/ProjectDto';
import { MessageService } from 'primeng/api';
import { BsModalService } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-project-item',
  templateUrl: './project-item.component.html',
  styleUrls: ['./project-item.component.scss']
})
export class ProjectItemComponent implements OnInit {
  @Input() project?: ProjectDto;
  public time = signal<string>('');

  constructor(private messageService: MessageService,
    private modalService: BsModalService,
    private projectService: ProjectService) { }

  ngOnInit(): void {
    if (this.project?.dueTime) {
      this.time.update(() => this.formatTime(this.project?.dueTime!))
    }
  }

  formatTime(inputTime: string): string {
    const [hours, minutes] = inputTime.split(':');
    return `${hours}:${minutes}`;
  }
}
