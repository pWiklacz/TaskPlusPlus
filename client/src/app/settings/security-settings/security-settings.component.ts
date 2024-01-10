import { Component } from '@angular/core';
import { SettingsService } from '../settings.service';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AccountService } from 'src/app/account/account.service';
import { UserStoreService } from 'src/app/account/user-store.service';
import { MustMatch } from 'src/app/shared/validators/passwords-must-match-validator';

@Component({
  selector: 'app-security-settings',
  templateUrl: './security-settings.component.html',
  styleUrls: ['./security-settings.component.scss']
})
export class SecuritySettingsComponent {
  submitted = false;
  complexPassword = '(?=^.{6,10}$)(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&*()_+{}":;\'?/>.<,])(?!.*\\s).*$';

  securitySettingsForm = new FormGroup({
    oldPassword: new FormControl('', Validators.required),
    password: new FormControl('', [Validators.required, Validators.pattern(this.complexPassword)]),
    confirmPassword: new FormControl('', Validators.required)
  }, {
    validators: MustMatch('password', 'confirmPassword')
  });

  constructor(settingsService: SettingsService,
    private accountService: AccountService,
    public userStoreService: UserStoreService) {
    settingsService.selectSettingsPage('security');
  }

  get form() { return this.securitySettingsForm.controls; }

  onSubmit() {
    this.submitted = true;
    if (this.securitySettingsForm.invalid) {
      return;
    }
  }
}
 