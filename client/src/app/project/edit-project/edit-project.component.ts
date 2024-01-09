import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { MessageService } from 'primeng/api';
import { CategoryService } from 'src/app/category/category.service';
import { ThemeService } from 'src/app/core/services/theme.service';
import { ProjectDto } from 'src/app/shared/models/project/ProjectDto';
import { ProjectService } from '../project.service';
import { EditProjectDto } from 'src/app/shared/models/project/EditProjectDto';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-edit-project',
  templateUrl: './edit-project.component.html',
  styleUrls: ['./edit-project.component.scss']
})
export class EditProjectComponent implements OnInit {
  project?: ProjectDto;
  editProjectForm!: FormGroup;
  addTimeChecked: boolean = false;
  defaultDate?: Date;

  constructor(public bsModalRef: BsModalRef,
    private themeService: ThemeService,
    public categoryService: CategoryService,
    private messageService: MessageService,
    private projectService: ProjectService) { }

  ngOnInit(): void {
    document.documentElement.style.setProperty('--calendar-body-color',
      this.themeService.getBodyColor());
    document.documentElement.style.setProperty('--calendar-bg-color',
      this.themeService.getInputBackgroundColor());
    document.documentElement.style.setProperty('--primary-color',
      this.themeService.getPrimaryColor());
    document.documentElement.style.setProperty('--secondary-color',
      this.themeService.getSecondaryColor());

    let dueDate: Date;

    if (this.project?.dueTime)
      this.addTimeChecked = true;

    if (this.project?.dueTime)
      dueDate = new Date(this.project?.dueDate! + ' ' + this.project?.dueTime)
    else
      dueDate = new Date(this.project?.dueDate!)

    this.defaultDate = new Date()

    this.editProjectForm = new FormGroup({
      name: new FormControl(this.project?.name, Validators.required),
      notes: new FormControl(this.project?.notes),
      date: new FormControl<Date | null>(this.project?.dueDate ? dueDate : null),
    });

  }

  get form() { return this.editProjectForm.controls; }


  onSubmit() {
    const formValues = this.editProjectForm.value;
    let formattedDate = null;
    let formattedTime = null;
    if (formValues.date) formattedDate = `${formValues.date.getFullYear()}-${(formValues.date.getMonth() + 1).toString().padStart(2, '0')}-${formValues.date.getDate().toString().padStart(2, '0')}`;
    if (formValues.date && this.addTimeChecked) formattedTime = `${formValues.date.getHours().toString().padStart(2, '0')}:${formValues.date.getMinutes().toString().padStart(2, '0')}:${formValues.date.getSeconds().toString().padStart(2, '0')}`;

    const updatedProject: EditProjectDto = {
      id: this.project?.id!,
      name: formValues.name!,
      dueDate: formattedDate!,
      notes: formValues.notes!,
      dueTime: formattedTime!,
      isCompleted: this.project?.isCompleted!
    }

    this.projectService.putProject(updatedProject).subscribe({
      next: (response: any) => {
        this.messageService.add({ severity: 'success', summary: 'Success', detail: response.message, life: 3000 });
        const project: ProjectDto = {
          id: this.project?.id!,
          name: formValues.name!,
          dueDate: formattedDate!,
          notes: formValues.notes!,
          dueTime: formattedTime!,
          isCompleted: this.project?.isCompleted!,
          completedOnUtc: null,
          tasks: this.project?.tasks!
        }
        this.projectService.updateProject(project);
      },
      error: (err: HttpErrorResponse) => {
        this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Problem with updating project', life: 3000 });
      }
    })

    this.bsModalRef.hide()
  }

  addTime() {
    let date: Date = this.editProjectForm.value.date;
    if (date) {
      const currentDate = new Date();
      const currentMinutes = currentDate.getMinutes();
      const nextMultipleOf5Minutes = Math.ceil(currentMinutes / 5) * 5;
      date.setHours(currentDate.getHours())
      date.setMinutes(nextMultipleOf5Minutes);
      this.editProjectForm.get('date')?.setValue(date);
      this.addTimeChecked = !this.addTimeChecked;
    }
  }

}
