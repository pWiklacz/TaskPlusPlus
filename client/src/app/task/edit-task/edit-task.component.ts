import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { MessageService } from 'primeng/api';
import { CategoryService } from 'src/app/category/category.service';
import { ThemeService } from 'src/app/core/services/theme.service';
import { CategoryDto } from 'src/app/shared/models/category/CategoryDto';
import { EnergyEnum } from 'src/app/shared/models/task/EnergyEnum';
import { PriorityEnum } from 'src/app/shared/models/task/PriorityEnum';
import { TagService } from 'src/app/tag/tag.service';
import { TaskService } from '../task.service';
import { TaskDto } from 'src/app/shared/models/task/TaskDto';
import { GroupingOptionsEnum } from 'src/app/shared/models/task/GroupingOptionsEnum';
import { AddTaskTimeConflictModalComponent } from '../add-task-time-conflict-modal/add-task-time-conflict-modal.component';
import { Time } from '@angular/common';
import { EditTaskDto } from 'src/app/shared/models/task/EditTaskDto';
import { HttpErrorResponse } from '@angular/common/http';
import { ProjectService } from 'src/app/project/project.service';

@Component({
  selector: 'app-edit-task',
  templateUrl: './edit-task.component.html',
  styleUrls: ['./edit-task.component.scss']
})
export class EditTaskComponent {
  task?: TaskDto;
  editTaskForm!: FormGroup;
  public EnergyEnum = EnergyEnum
  public PriorityEnum = PriorityEnum
  public systemCategories: CategoryDto[] = [];
  addTimeChecked: boolean = false;
  defaultDate?: Date;
  isEdited = false;
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

  constructor(public bsModalRef: BsModalRef,
    private themeService: ThemeService,
    public categoryService: CategoryService,
    private messageService: MessageService,
    public tagService: TagService,
    private taskService: TaskService,
    protected modalService: BsModalService,
    public projectService: ProjectService) { }

  ngOnInit() {
    document.documentElement.style.setProperty('--calendar-body-color',
      this.themeService.getBodyColor());
    document.documentElement.style.setProperty('--calendar-bg-color',
      this.themeService.getInputBackgroundColor());
    document.documentElement.style.setProperty('--primary-color',
      this.themeService.getPrimaryColor());
    document.documentElement.style.setProperty('--secondary-color',
      this.themeService.getSecondaryColor());

    if (this.task?.dueTime)
      this.addTimeChecked = true;

    let dueDate: Date;

    if (this.task?.dueTime)
      dueDate = new Date(this.task?.dueDate! + ' ' + this.task?.dueTime)
    else
      dueDate = new Date(this.task?.dueDate!)
    const tagsIds = this.task?.tags.flatMap(tag => tag.id ?? []);

    this.editTaskForm = new FormGroup({
      name: new FormControl(this.task?.name, Validators.required),
      notes: new FormControl(this.task?.notes),
      date: new FormControl<Date | null>(this.task?.dueDate ? dueDate : null),
      energy: new FormControl(this.task?.energy.toString()),
      priority: new FormControl(this.task?.priority.toString()),
      durationTime: new FormControl(this.task?.durationTime.toString()),
      selectedTags: new FormControl(tagsIds),
      categoryId: new FormControl(this.task?.categoryId.toString()),
      projectId: new FormControl(this.task?.projectId?.toString())
    });

    this.editTaskForm.get('energy')!.valueChanges.subscribe((selectedValue) => {
      if (selectedValue == 0) {
        this.editTaskForm.get('energy')!.setValue(null, { emitEvent: false });
      }
    });
    this.editTaskForm.get('priority')!.valueChanges.subscribe((selectedValue) => {
      if (selectedValue == 1) {
        this.editTaskForm.get('priority')!.setValue(null, { emitEvent: false });
      }
    });
    this.editTaskForm.get('durationTime')!.valueChanges.subscribe((selectedValue) => {
      if (selectedValue == '') {
        this.editTaskForm.get('durationTime')!.setValue(null, { emitEvent: false });
      }
    });
    this.tagService.getTags()?.subscribe({
      error: error => console.log(error)
    })
    this.projectService.getProjects()?.subscribe({
      error: error => console.log(error)
    });
    this.systemCategories = this.categoryService.systemCategories.filter(category => ![0].includes(category.id));
    this.defaultDate = new Date()

    this.editTaskForm.valueChanges.subscribe((val) => {
      if (val.name !== this.task?.name
        || val.notes !== this.task?.notes
        || val.date !== this.task?.dueDate
        || val.energy !== this.task?.energy
        || val.priority !== this.task?.priority
        || val.durationTime !== this.task?.durationTime
        || !this.compareTags(val.selectedTags, this.task?.tags!)
        || val.categoryId !== this.task?.categoryId
        || val.projectId !== this.task?.projectId) {
        this.isEdited = true;
      } else {
        this.isEdited = false;
      }
    });
  }

  compareTags(selectedTags: any[], taskTags: any[]): boolean {
    if (selectedTags.length !== taskTags.length) {
      return false;
    }
    for (let i = 0; i < selectedTags.length; i++) {
      if (selectedTags[i].id !== taskTags[i].id) {
        return false;
      }
    }
    return true;
  }

  get form() { return this.editTaskForm.controls; }

  async onSubmit() {
    const formValues = this.editTaskForm.value;
    let formattedDate = null;
    let formattedTime = null;
    if (formValues.date) formattedDate = `${formValues.date.getFullYear()}-${(formValues.date.getMonth() + 1).toString().padStart(2, '0')}-${formValues.date.getDate().toString().padStart(2, '0')}`;
    if (formValues.date && this.addTimeChecked) formattedTime = `${formValues.date.getHours().toString().padStart(2, '0')}:${formValues.date.getMinutes().toString().padStart(2, '0')}:${formValues.date.getSeconds().toString().padStart(2, '0')}`;
    const udpatedTask: EditTaskDto = {
      id: this.task?.id!,
      name: formValues.name!,
      dueDate: formattedDate!,
      notes: formValues.notes!,
      durationTime: formValues.durationTime ?? 0,
      dueTime: formattedTime!,
      priority: formValues.priority ?? PriorityEnum.E.id,
      energy: formValues.energy ?? EnergyEnum.NONE.id,
      projectId: formValues.projectId,
      categoryId: formValues.categoryId,
      tags: formValues.selectedTags,
      isCompleted: this.task?.isCompleted!
    }
    let isConflict = false;
    if (formattedTime) {
      isConflict = true;
      this.taskService.QueryParams().date = formattedDate!;
      this.taskService.QueryParams().categoryId = -1;
      this.taskService.QueryParams().groupBy = GroupingOptionsEnum.NONE;
      this.taskService.getTaskWithNoCategory()?.subscribe(
        {
          next: (response: any) => {
            const existingTasks: any[] = response.value['All'];
            const newTaskStartTime = new Date(formattedDate! + ' ' + formattedTime!);
            const newTaskEndTime = new Date(newTaskStartTime.getTime() + formValues.durationTime * 60000);
            const collision = existingTasks.some((existingTask: any) => {
              const existingTaskStartTime = new Date(existingTask.dueDate + ' ' + existingTask.dueTime);
              const existingTaskEndTime = new Date(existingTaskStartTime.getTime() + existingTask.durationTime * 60000);
              return (
                (newTaskStartTime >= existingTaskStartTime && newTaskStartTime <= existingTaskEndTime) ||
                (newTaskEndTime >= existingTaskStartTime && newTaskEndTime <= existingTaskEndTime) ||
                (newTaskStartTime <= existingTaskStartTime && newTaskEndTime >= existingTaskEndTime)
              );
            });
            if (collision) {
              const modalRef = this.modalService.show(AddTaskTimeConflictModalComponent);
              modalRef.onHidden!.subscribe((result) => {
                isConflict = !(result === 'confirm');
                if (!isConflict) {
                  this.PutTask(udpatedTask);
                  this.bsModalRef.hide()
                }
              });
            } else {
              this.PutTask(udpatedTask);
              this.bsModalRef.hide()
            }
            this.taskService.UserTasks().delete(-1);
          },
          error: error => console.log(error)
        }
      );
    } else {
      this.PutTask(udpatedTask);
      this.bsModalRef.hide()
    }
  }

  private PutTask(updatedTask: EditTaskDto) {
    this.taskService.putTask(updatedTask).subscribe({
      next: (response: any) => {
        this.messageService.add({ severity: 'success', summary: 'Success', detail: response.message, life: 3000 });
        const task: TaskDto = {
          id: updatedTask.id,
          name: updatedTask.name,
          dueDate: updatedTask.dueDate,
          notes: updatedTask.notes,
          isCompleted: false,
          dueTime: updatedTask.dueTime,
          durationTime: +updatedTask.durationTime,
          priority: +updatedTask.priority,
          energy: +updatedTask.energy,
          projectId: updatedTask.projectId,
          categoryId: updatedTask.categoryId,
          completedOnUtc: null,
          tags: this.tagService.filterTagsByTagIds(updatedTask.tags)
        };
        if (task.projectId) {

          this.projectService.editTaskInProject(task.projectId, task);
        }

        this.taskService.updateTask(task);
      },
      error: (err: HttpErrorResponse) => {
        this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Problem with editing task', life: 3000 });
      }
    });
  }

  private parseTimeString(timeString: string): Time | null {
    const timeRegex = /^(\d{2}):(\d{2}):(\d{2})$/;
    if (timeString) {
      const match = timeString.match(timeRegex);
      if (match) {
        const [, hours, minutes, seconds] = match.map(Number);
        return { hours, minutes };
      }
    }
    return null;
  }

  addTime() {
    let date: Date = this.editTaskForm.value.date;
    if (date) {
      const currentDate = new Date();
      const currentMinutes = currentDate.getMinutes();
      const nextMultipleOf5Minutes = Math.ceil(currentMinutes / 5) * 5;
      date.setHours(currentDate.getHours())
      date.setMinutes(nextMultipleOf5Minutes);
      this.editTaskForm.get('date')?.setValue(date);
      this.addTimeChecked = !this.addTimeChecked;
    }
  }

  openConflictModal(): boolean {
    this.bsModalRef = this.modalService.show(AddTaskTimeConflictModalComponent);
    let isConfirmed = false;
    this.bsModalRef.content.Confirmed.subscribe((confirmed: boolean) => {
      isConfirmed = confirmed;
    })

    return isConfirmed;
  }
}
