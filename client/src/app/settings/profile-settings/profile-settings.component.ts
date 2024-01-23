import { Component, OnInit } from '@angular/core';
import { SettingsService } from '../settings.service';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AccountService } from 'src/app/account/account.service';
import { UserStoreService } from 'src/app/account/user-store.service';
import { MessageService } from 'primeng/api';
import { ThemeService } from 'src/app/core/services/theme.service';
import { UpdateProfileDto } from 'src/app/shared/models/account/UpdateProfileDto';

@Component({
  selector: 'app-profile-settings',
  templateUrl: './profile-settings.component.html',
  styleUrls: ['./profile-settings.component.scss']
})
export class ProfileSettingsComponent implements OnInit {
  submitted = false;
  isEdited: boolean = false;

  editProfileForm = new FormGroup({
    firstName: new FormControl(this.userStoreService.firstName(), Validators.required),
    lastName: new FormControl(this.userStoreService.lastName(), Validators.required)
  })

  constructor(settingsService: SettingsService,
    private accountService: AccountService,
    public userStoreService: UserStoreService,
    private messageService: MessageService,
    private themeService: ThemeService) {
    settingsService.selectSettingsPage('profile');
  }

  ngOnInit(): void {
    document.documentElement.style.setProperty('--border-color',
      this.themeService.getBorderColor());
    document.documentElement.style.setProperty('--input-bg-color',
      this.themeService.getInputBackgroundColor());

      this.editProfileForm.valueChanges.subscribe((val) => {
        if (val.firstName !== this.userStoreService.firstName()
          || val.lastName !== this.userStoreService.lastName()) {
          this.isEdited = true;
        } else {
          this.isEdited = false;
        }
      })
  }

  get form() { return this.editProfileForm.controls; }

  onSubmit() {
    this.submitted = true;
    if (this.editProfileForm.invalid) {
      return;
    }

    const formValues = this.editProfileForm.value;

    const updateProfileDto : UpdateProfileDto = {
      userId: this.userStoreService.uid(),
      firstName: formValues.firstName!,
      lastName: formValues.lastName!
    }

    this.accountService.updateProfileInformation(updateProfileDto).subscribe({
      next: (response) => {
        this.submitted = false;
        this.isEdited = false;
        this.messageService.add({ severity: 'success', summary: 'Success', detail: response.message, life: 3000 });
      },
      error: (response) => {
        console.error(response.message);
        this.onReset()
        this.messageService.add({ severity: 'error', summary: 'Error', detail: response.message, life: 3000 });}
    })
  }

  onReset() {
    this.isEdited = false;
    this.editProfileForm = new FormGroup({
      firstName: new FormControl(this.userStoreService.firstName(), Validators.required),
      lastName: new FormControl(this.userStoreService.lastName(), Validators.required)
    })
  }
}
