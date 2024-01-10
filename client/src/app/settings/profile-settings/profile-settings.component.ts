import { Component, OnInit } from '@angular/core';
import { SettingsService } from '../settings.service';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AccountService } from 'src/app/account/account.service';
import { UserStoreService } from 'src/app/account/user-store.service';

@Component({
  selector: 'app-profile-settings',
  templateUrl: './profile-settings.component.html',
  styleUrls: ['./profile-settings.component.scss']
})
export class ProfileSettingsComponent implements OnInit {
  submitted = false;
  editProfileForm = new FormGroup({
    firstName: new FormControl(this.userStoreService.firstName(), Validators.required),
    lastName: new FormControl(this.userStoreService.lastName(), Validators.required)
  })

  constructor(settingsService: SettingsService,
    private accountService: AccountService,
    public userStoreService: UserStoreService) {
    settingsService.selectSettingsPage('profile');
  }

  ngOnInit(): void {
  }

  get form() { return this.editProfileForm.controls; }

  onSubmit() {
    this.submitted = true;
    if (this.editProfileForm.invalid) {
      return;
    }
  }

  onReset()
  {
    this.editProfileForm = new FormGroup({
      firstName: new FormControl(this.userStoreService.firstName(), Validators.required),
      lastName: new FormControl(this.userStoreService.lastName(), Validators.required)
    })
  }
}
