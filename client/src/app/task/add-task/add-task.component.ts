import { KeyValue } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { MessageService } from 'primeng/api';
import { Observable } from 'rxjs';
import { CategoryService } from 'src/app/category/category.service';
import { ThemeService } from 'src/app/core/services/theme.service';
import { EnergyEnum } from 'src/app/shared/models/task/EnergyEnum';
import { PriorityEnum } from 'src/app/shared/models/task/PriorityEnum';
import { TagService } from 'src/app/tag/tag.service';

interface Tag {
  value: number;
  label: string;
}

@Component({
  selector: 'app-add-task',
  templateUrl: './add-task.component.html',
  styleUrls: ['./add-task.component.scss']
})
export class AddTaskComponent implements OnInit {
  addTaskForm!: FormGroup;
  public EnergyEnum = EnergyEnum
  public PriorityEnum = PriorityEnum
  public systemCategories :{ id: string; name: string; icon: string; color: string; }[] = [];

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

  // tags: Tag[] = [
  //   { value: 5, label: "tagA" },
  //   { value: 10, label: "tagB" },
  //   { value: 11, label: "tagC" },
  //   { value: 12, label: "tagD" },
  //   { value: 13, label: "tagE" },
  //   { value: 14, label: "tagF" },
  // ]

  selectedTaags: Tag[] = [];

  constructor(public bsModalRef: BsModalRef,
    private modalService: BsModalService,
    private themeService: ThemeService,
    public categoryService: CategoryService,
    private messageService: MessageService,
    public tagService: TagService) { }

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

   this.systemCategories = this.categoryService.systemCategories.filter(category => !['2', '3', '5'].includes(category.id));
  }

  get form() { return this.addTaskForm.controls; }

  onSubmit() {
    const formValues = this.addTaskForm.value;
   
    console.log(formValues.date)
  }
}
