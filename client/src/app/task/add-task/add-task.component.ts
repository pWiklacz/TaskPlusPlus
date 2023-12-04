import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { MessageService } from 'primeng/api';
import { CategoryService } from 'src/app/category/category.service';
import { ThemeService } from 'src/app/core/services/theme.service';
import { EnergyEnum } from 'src/app/shared/models/EnergyEnum';
import { PriorityEnum } from 'src/app/shared/models/PriorityEnum';

@Component({
  selector: 'app-add-task',
  templateUrl: './add-task.component.html',
  styleUrls: ['./add-task.component.scss']
})
export class AddTaskComponent implements OnInit {
  addTaskForm!: FormGroup;
  public EnergyEnum = EnergyEnum
  public PriorityEnum = PriorityEnum
  date?: Date = new Date;

  constructor(public bsModalRef: BsModalRef,
    private themeService: ThemeService,
    private categoryService: CategoryService,
    private messageService: MessageService) { }

  ngOnInit() {
    document.documentElement.style.setProperty('--calendar-body-color', 
    this.themeService.getBodyColor());
    document.documentElement.style.setProperty('--calendar-bg-color', 
    this.themeService.getInputBackgroundColor());
    console.log(this.date)
    this.date?.setMinutes(30);
    console.log(this.date)
  
    this.addTaskForm = new FormGroup({
      name: new FormControl('', Validators.required),
      notes: new FormControl(''),
      date: new FormControl<Date | null>(null),
      energy: new FormControl('4'),
      priority: new FormControl('6'),
      durationTime: new FormControl('0')
    });

    this.addTaskForm.get('energy')!.valueChanges.subscribe((selectedValue) => {
      if (selectedValue == 0) {
        this.addTaskForm.get('energy')!.setValue(4, { emitEvent: false });
      }
    });
    this.addTaskForm.get('priority')!.valueChanges.subscribe((selectedValue) => {
      if (selectedValue == 1) {
        this.addTaskForm.get('priority')!.setValue(6, { emitEvent: false });
      }
    });
    this.addTaskForm.get('durationTime')!.valueChanges.subscribe((selectedValue) => {
      if (selectedValue == '') {
        this.addTaskForm.get('durationTime')!.setValue(0, { emitEvent: false });
      }
    });
  }


  onSubmit() {

  }
}
