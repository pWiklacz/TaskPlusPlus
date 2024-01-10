import { Component } from '@angular/core';
import { SettingsService } from '../settings.service';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { UserStoreService } from 'src/app/account/user-store.service';

@Component({
  selector: 'app-account-settings',
  templateUrl: './account-settings.component.html',
  styleUrls: ['./account-settings.component.scss']
})
export class AccountSettingsComponent {
  submitted = false;

  AccountSettingsForm = new FormGroup({
    email: new FormControl(this.userStoreService.email(), [Validators.required, Validators.email]),
  })

  constructor(private settingsService: SettingsService,
    public userStoreService: UserStoreService) {
    settingsService.selectSettingsPage('account');
  }

  get form() { return this.AccountSettingsForm.controls; }

  onSubmit() {
    this.submitted = true;
    if (this.AccountSettingsForm.invalid) {
      return;
    }
  }

  onReset() {
    this.AccountSettingsForm = new FormGroup({
      email: new FormControl(this.userStoreService.email(), [Validators.required, Validators.email]),
    })
  }
}
