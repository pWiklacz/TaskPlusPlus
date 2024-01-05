import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { MessageService } from 'primeng/api';
import { CategoryService } from 'src/app/category/category.service';
import { ThemeService } from 'src/app/core/services/theme.service';
import { CreateProjectDto } from 'src/app/shared/models/project/CreateProjectDto';
import { TaskService } from 'src/app/task/task.service';
import { ProjectService } from '../project.service';
import { HttpErrorResponse } from '@angular/common/http';
import { ProjectDto } from 'src/app/shared/models/project/ProjectDto';

@Component({
  selector: 'app-add-project',
  templateUrl: './add-project.component.html',
  styleUrls: ['./add-project.component.scss']
})
export class AddProjectComponent implements OnInit {
  addProjectForm!: FormGroup;
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

    this.addProjectForm = new FormGroup({
      name: new FormControl('', Validators.required),
      notes: new FormControl(''),
      date: new FormControl<Date | null>(null),
    });

    this.defaultDate = new Date()
  }

  get form() { return this.addProjectForm.controls; }

  onSubmit() {
    const formValues = this.addProjectForm.value;
    let formattedDate = null;
    let formattedTime = null;
    if (formValues.date) formattedDate = `${formValues.date.getFullYear()}-${(formValues.date.getMonth() + 1).toString().padStart(2, '0')}-${formValues.date.getDate().toString().padStart(2, '0')}`;
    if (formValues.date && this.addTimeChecked) formattedTime = `${formValues.date.getHours().toString().padStart(2, '0')}:${formValues.date.getMinutes().toString().padStart(2, '0')}:${formValues.date.getSeconds().toString().padStart(2, '0')}`;
    const createdProject: CreateProjectDto = {
      name: formValues.name!,
      dueDate: formattedDate!,
      notes: formValues.notes!,
      dueTime: formattedTime!,
    }

    this.projectService.postProject(createdProject).subscribe({
      next: (response: any) => {
        this.messageService.add({ severity: 'success', summary: 'Success', detail: response.message, life: 3000 });
        const project: ProjectDto = {
          id: response.value,
          name: createdProject.name,
          notes: createdProject.notes,
          dueDate: createdProject.dueDate,
          dueTime: createdProject.dueTime,
          isCompleted: false,
          completedOnUtc: null,
          tasks: []
        }
        this.projectService.addProject(project);
      },
      error: (err: HttpErrorResponse) => {
        this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Problem with creating project', life: 3000 });
      }
    })

    this.bsModalRef.hide()
  }

  addTime() {
    let date: Date = this.addProjectForm.value.date;
    if (date) {
      const currentDate = new Date();
      const currentMinutes = currentDate.getMinutes();
      const nextMultipleOf5Minutes = Math.ceil(currentMinutes / 5) * 5;
      date.setHours(currentDate.getHours())
      date.setMinutes(nextMultipleOf5Minutes);
      this.addProjectForm.get('date')?.setValue(date);
      this.addTimeChecked = !this.addTimeChecked;
    }
  }
}
