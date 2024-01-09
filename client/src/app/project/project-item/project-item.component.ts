import { Component, Input, OnInit, signal } from '@angular/core';
import { ProjectService } from '../project.service';
import { ProjectDto } from 'src/app/shared/models/project/ProjectDto';
import { MessageService } from 'primeng/api';
import { BsModalRef, BsModalService, ModalOptions } from 'ngx-bootstrap/modal';
import { EditProjectComponent } from '../edit-project/edit-project.component';
import { DeleteConfirmationModalComponent } from 'src/app/shared/components/delete-confirmation-modal/delete-confirmation-modal.component';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-project-item',
  templateUrl: './project-item.component.html',
  styleUrls: ['./project-item.component.scss']
})
export class ProjectItemComponent implements OnInit {
  @Input() project?: ProjectDto;
  public time = signal<string>('');
  bsModalRef?: BsModalRef;

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

  complete()
  {
    this.projectService.changeCompleteStatus(this.project!.isCompleted, this.project!.id).subscribe({
      next: (response: any) => {
        this.projectService.updateProject(this.project!);    
        this.messageService.add({ severity: 'success', summary: 'Success', detail: response.message, life: 3000 });
      },
      error: (err: HttpErrorResponse) => {
        this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Problem with compliting a project', life: 3000 });
      }
    })
  }

  openEditProjectModal() {
    const initialState: ModalOptions = {
      initialState: {
        project: this.project
      },
      backdrop: 'static',
      class: 'modal-dialog-centered'
    }
    this.bsModalRef = this.modalService.show(EditProjectComponent, initialState);
  }

  openDeleteProjectModal() {
    const initialState: ModalOptions = {
      initialState: {
        name: this.project?.name
      }
    }
    this.bsModalRef = this.modalService.show(DeleteConfirmationModalComponent, initialState);
    this.bsModalRef.content.deleteConfirmed.subscribe((deleteConfirmed: boolean) => {
      if (deleteConfirmed) {
        this.projectService.deleteProject(this.project?.id!).subscribe({
          next: (response: any) => {
            this.messageService.add({ severity: 'success', summary: 'Success', detail: response.message, life: 3000 });
            this.projectService.removeProject(this.project?.id!);
          },
          error: (err: HttpErrorResponse) => {
            this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Problem with deleting project', life: 3000 });
          }
        })
      }
    });
  }
}
