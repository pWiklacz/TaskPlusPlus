import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { MessageService } from 'primeng/api';
import { CategoryService } from 'src/app/category/category.service';
import { ThemeService } from 'src/app/core/services/theme.service';
import { CreateTaskDto } from 'src/app/shared/models/task/CreateTaskDto';
import { EnergyEnum } from 'src/app/shared/models/task/EnergyEnum';
import { PriorityEnum } from 'src/app/shared/models/task/PriorityEnum';
import { TagService } from 'src/app/tag/tag.service';
import { TaskService } from '../task.service';
import { HttpErrorResponse } from '@angular/common/http';
import { TaskDto } from 'src/app/shared/models/task/TaskDto';
import { Time } from "@angular/common"

interface Tag {
  value: number;
  label: string;
}

@Component({
  selector: 'app-add-tag',
  templateUrl: './add-task.component.html',
  styleUrls: ['./add-task.component.scss']
})
export class AddTaskComponent implements OnInit {
  addTaskForm!: FormGroup;
  public EnergyEnum = EnergyEnum
  public PriorityEnum = PriorityEnum
  public systemCategories: { id: string; name: string; icon: string; color: string; }[] = [];

  durationTimes = [
    { value: null, label: "None" },
    { value: 5, label: "5 min" },
    { value: 10, label: "10 min" },
    { value: 15, label: "15 min" },
    { value: 30, label: "30 min" },
    { value: 45, label: "45 min" },
    { value: 60, label: "1h" },
    { value: 120, label: "2h" },
    { value: 240, label: "4h" },
    { value: 480, label: "8h" },
  ];

  selectedTaags: Tag[] = [];

  constructor(public bsModalRef: BsModalRef,
    private themeService: ThemeService,
    public categoryService: CategoryService,
    private messageService: MessageService,
    public tagService: TagService,
    private taskService: TaskService) { }

  ngOnInit() {
    document.documentElement.style.setProperty('--calendar-body-color',
      this.themeService.getBodyColor());
    document.documentElement.style.setProperty('--calendar-bg-color',
      this.themeService.getInputBackgroundColor());
    document.documentElement.style.setProperty('--primary-color',
      this.themeService.getPrimaryColor());
    document.documentElement.style.setProperty('--secondary-color',
      this.themeService.getSecondaryColor());

    this.addTaskForm = new FormGroup({
      name: new FormControl('', Validators.required),
      notes: new FormControl(''),
      date: new FormControl<Date | null>(null),
      energy: new FormControl(),
      priority: new FormControl(),
      durationTime: new FormControl(),
      selectedTags: new FormControl([]),
      categoryId: new FormControl('1'),
      projectId: new FormControl()
    });


    this.addTaskForm.get('energy')!.valueChanges.subscribe((selectedValue) => {
      if (selectedValue == 0) {
        this.addTaskForm.get('energy')!.setValue(null, { emitEvent: false });
      }
    });
    this.addTaskForm.get('priority')!.valueChanges.subscribe((selectedValue) => {
      if (selectedValue == 1) {
        this.addTaskForm.get('priority')!.setValue(null, { emitEvent: false });
      }
    });
    this.addTaskForm.get('durationTime')!.valueChanges.subscribe((selectedValue) => {
      if (selectedValue == '') {
        this.addTaskForm.get('durationTime')!.setValue(null, { emitEvent: false });
      }
    });


    this.tagService.getTags()?.subscribe({
      error: error => console.log(error)
    })

    this.systemCategories = this.categoryService.systemCategories.filter(category => !['99'].includes(category.id));
  }

  get form() { return this.addTaskForm.controls; }

  onSubmit() {
    const formValues = this.addTaskForm.value;
    let formattedDate = null;
    let formattedTime = null;
    if (formValues.date) formattedDate = `${formValues.date.getFullYear()}-${(formValues.date.getMonth() + 1).toString().padStart(2, '0')}-${formValues.date.getDate().toString().padStart(2, '0')}`;
    if (formValues.date) formattedTime = `${formValues.date.getHours().toString().padStart(2, '0')}:${formValues.date.getMinutes().toString().padStart(2, '0')}:${formValues.date.getSeconds().toString().padStart(2, '0')}`;
    const createdTask: CreateTaskDto = {
      name: formValues.name!,
      dueDate: formattedDate!,
      notes: formValues.notes!,
      durationTime: formValues.durationTime ?? 0,
      dueTime: formattedTime!,
      priority: formValues.priority ?? PriorityEnum.E.id,
      energy: formValues.energy ?? EnergyEnum.NONE.id,
      projectId: formValues.projectId,
      categoryId: formValues.categoryId,
      tags: formValues.selectedTags
    }

    this.taskService.postTask(createdTask).subscribe({
      next: (response: any) => {
        this.messageService.add({ severity: 'success', summary: 'Success', detail: response.message, life: 3000 });
        const task: TaskDto = {
          id: response.value,
          name: createdTask.name,
          dueDate: new Date(createdTask.dueDate),
          notes: createdTask.notes,
          isCompleted: false,
          dueTime: this.parseTimeString(createdTask.dueTime)!,
          durationTime: +createdTask.durationTime,
          priority: +createdTask.priority,
          energy: +createdTask.energy,
          projectId: createdTask.projectId,
          categoryId: createdTask.categoryId,
          completedOnUtc: null,
          tags: this.tagService.filterTagsByTagIds(createdTask.tags)
        }
        console.log(task)
        this.taskService.addTask(task);

      },
      error: (err: HttpErrorResponse) => {
        this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Problem with creating task', life: 3000 });
      }
    })

    this.bsModalRef.hide()
  }

  private parseTimeString(timeString: string): Time | null {
    const timeRegex = /^(\d{2}):(\d{2}):(\d{2})$/;
    if(timeString)
    {
      const match = timeString.match(timeRegex);

      if (match) {
        const [, hours, minutes, seconds] = match.map(Number);
        return { hours, minutes };
      } 
    }
    //console.error('Invalid time string format or values.');
    return null;
  }
}
