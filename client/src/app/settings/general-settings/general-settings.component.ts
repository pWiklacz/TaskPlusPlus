import { Component, OnInit } from '@angular/core';
import { SettingsService } from '../settings.service';
import { FormControl, FormGroup } from '@angular/forms';
import { CategoryDto } from 'src/app/shared/models/category/CategoryDto';
import { CategoryService } from 'src/app/category/category.service';
import { AccountService } from 'src/app/account/account.service';
import { ThemeService } from 'src/app/core/services/theme.service';
import { UpdateUserSettingsDto } from 'src/app/shared/models/account/UpdateUserSettingsDto';
import { UserStoreService } from 'src/app/account/user-store.service';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-general-settings',
  templateUrl: './general-settings.component.html',
  styleUrls: ['./general-settings.component.scss']
})
export class GeneralSettingsComponent implements OnInit {
  generalSettingsForm!: FormGroup
  public systemCategories: CategoryDto[] = [];
  isEdited: boolean = false;

  constructor(private settingsService: SettingsService,
    public categoryService: CategoryService,
    private accountService: AccountService,
    private themeService: ThemeService,
    private userStoreService: UserStoreService) {
    settingsService.selectSettingsPage('general');
  }
  ngOnInit(): void {
    document.documentElement.style.setProperty('--calendar-body-color',
      this.themeService.getBodyColor());
    document.documentElement.style.setProperty('--calendar-bg-color',
      this.themeService.getInputBackgroundColor());
    document.documentElement.style.setProperty('--primary-color',
      this.themeService.getPrimaryColor());
    document.documentElement.style.setProperty('--secondary-color',
      this.themeService.getSecondaryColor());
    const userSettings = this.accountService.getUserSettings();
    this.generalSettingsForm = new FormGroup({
      startPage: new FormControl(userSettings?.startPage.toString()),
      language: new FormControl(userSettings?.language.toString()),
      dateFormat: new FormControl(userSettings?.dateFormat.toString()),
      timeFormat: new FormControl(userSettings?.timeFormat.toString()),
    })
    this.systemCategories = this.categoryService.systemCategories;
    this.generalSettingsForm.valueChanges.subscribe((val) => {
      if (val.startPage !== userSettings?.startPage
        || val.language !== userSettings?.language
        || val.dateFormat !== userSettings?.dateFormat
        || val.timeFormat !== userSettings?.timeFormat) {
        this.isEdited = true;
      } else {
        this.isEdited = false;
      }
    })
  }

  timeFormats = [
    { value: "12", label: "12H" },
    { value: "24", label: "24H" },
  ]

  languages = [
    { value: "ENG", label: "English" },
  ]

  dateFormats = [
    { value: "DD-MM-YYYY", label: "DD-MM-YYYY" },
    { value: "MM-DD-YYYY", label: "MM-DD-YYYY" },
  ]

  get form() { return this.generalSettingsForm.controls; }

  onSubmit() {
    const formValues = this.generalSettingsForm.value;
    const userSettings = this.accountService.getUserSettings();
    userSettings?.startPage && (
      userSettings.startPage = formValues.startPage!);
    userSettings?.language && (
      userSettings.language = formValues.language!);
    userSettings?.dateFormat && (
      userSettings.dateFormat = formValues.dateFormat!);
    userSettings?.timeFormat && (
      userSettings.timeFormat = formValues.timeFormat!);

    const userSettingsDto: UpdateUserSettingsDto = {
      UserId: this.userStoreService.uid(),
      settings: userSettings!
    };

    this.accountService.updateSettings(userSettingsDto).subscribe({
      next: () => {
        const userSettingsJson = JSON.stringify(userSettings);
        localStorage.setItem('settings', userSettingsJson);
        this.isEdited = false;
      },
      error: (err: HttpErrorResponse) => {
        console.log(err);
      }
    });
  }
}
