import { Component, OnInit } from '@angular/core';
import { SettingsService } from '../settings.service';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { UserStoreService } from 'src/app/account/user-store.service';
import { ThemeService } from 'src/app/core/services/theme.service';

@Component({
  selector: 'app-account-settings',
  templateUrl: './account-settings.component.html',
  styleUrls: ['./account-settings.component.scss']
})
export class AccountSettingsComponent implements OnInit {
  submitted = false;

  AccountSettingsForm = new FormGroup({
    email: new FormControl(this.userStoreService.email(), [Validators.required, Validators.email]),
  })

  constructor(private settingsService: SettingsService,
    public userStoreService: UserStoreService,
    private themeService: ThemeService) {
    settingsService.selectSettingsPage('account');
  }
  ngOnInit(): void {
    document.documentElement.style.setProperty('--border-color',
      this.themeService.getBorderColor());
    document.documentElement.style.setProperty('--input-bg-color',
      this.themeService.getInputBackgroundColor());
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
