import { Component, OnInit } from '@angular/core';
import { SettingsService } from '../settings.service';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AccountService } from 'src/app/account/account.service';
import { UserStoreService } from 'src/app/account/user-store.service';
import { MustMatch } from 'src/app/shared/validators/passwords-must-match-validator';
import { UpdatePasswordDto } from 'src/app/shared/models/account/UpdatePasswordDto';
import { HttpErrorResponse } from '@angular/common/http';
import { MessageService } from 'primeng/api';
import { AddPasswordDto } from 'src/app/shared/models/account/AddPasswordDto';
import { ThemeService } from 'src/app/core/services/theme.service';
import { changeTwoFactorEnabledStatusDto } from 'src/app/shared/models/account/changeTwoFactorEnabledStatusDto';

@Component({
  selector: 'app-security-settings',
  templateUrl: './security-settings.component.html',
  styleUrls: ['./security-settings.component.scss']
})
export class SecuritySettingsComponent implements OnInit {
  submitted = false;
  complexPassword = '(?=^.{6,10}$)(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&*()_+{}":;\'?/>.<,])(?!.*\\s).*$';
  isText: boolean = false;
  eyeIcon: string = "fa-eye-slash";
  type: string = "password";
  changePasswordForm = new FormGroup({
    oldPassword: new FormControl('', Validators.required),
    password: new FormControl('', [Validators.required, Validators.pattern(this.complexPassword)]),
    confirmPassword: new FormControl('', Validators.required)
  }, {
    validators: MustMatch('password', 'confirmPassword')
  });

  addPasswordForm = new FormGroup({
    password: new FormControl('', [Validators.required, Validators.pattern(this.complexPassword)]),
    confirmPassword: new FormControl('', Validators.required)
  }, {
    validators: MustMatch('password', 'confirmPassword')
  });

  constructor(settingsService: SettingsService,
    private accountService: AccountService,
    public userStoreService: UserStoreService,
    private messageService: MessageService,
    private themeService: ThemeService) {
    settingsService.selectSettingsPage('security');
  }

  ngOnInit(): void {
    document.documentElement.style.setProperty('--border-color',
      this.themeService.getBorderColor());
    document.documentElement.style.setProperty('--input-bg-color',
      this.themeService.getInputBackgroundColor());
  }

  get changeForm() { return this.changePasswordForm.controls; }
  get addForm() { return this.addPasswordForm.controls; }

  onChangePasswordSubmit() {
    this.submitted = true;
    if (this.changePasswordForm.invalid) {
      return;
    }
    const formValues = this.changePasswordForm.value;

    const updatePasswordDto: UpdatePasswordDto = {
      userId: this.userStoreService.uid(),
      newPassword: formValues.password!,
      currentPassword: formValues.oldPassword!
    }

    this.accountService.updatePassword(updatePasswordDto).subscribe({
      next: (response) => {
        this.submitted = false;
        this.addPasswordForm.reset();
        this.messageService.add({ severity: 'success', summary: 'Success', detail: response.message, life: 3000 });
      },
      error: (response) => {
        console.error(response.message);
        this.addPasswordForm.reset();
        this.messageService.add({ severity: 'error', summary: 'Error', detail: response.message, life: 3000 });
      }
    })
  }

  onAddPasswordSubmit() {
    this.submitted = true;
    if (this.addPasswordForm.invalid) {
      return;
    }

    const formValues = this.addPasswordForm.value;

    const addPasswordDto: AddPasswordDto = {
      userId: this.userStoreService.uid(),
      password: formValues.password!,
    }

    this.accountService.addPassword(addPasswordDto).subscribe({
      next: (response) => {
        this.submitted = false;
        this.userStoreService.setHasPassword(true);
        this.addPasswordForm.reset();
        this.messageService.add({ severity: 'success', summary: 'Success', detail: response.message, life: 3000 });
      },
      error: (err: HttpErrorResponse) => {
        console.error(err);
        this.addPasswordForm.reset();
        this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Problem updating password', life: 3000 });
      }
    })
  }

  twoFactorSubmit(status: boolean) {
    const twoFactorEnabledStatusDto: changeTwoFactorEnabledStatusDto = {
      userId: this.userStoreService.uid(),
      twoFactorEnabled: status
    }

    this.accountService.changeTwoFactorEnabledStatus(twoFactorEnabledStatusDto).subscribe({
      next: (response) => {
        this.userStoreService.setTwoFactorEnabled(status);
        this.messageService.add({ severity: 'success', summary: 'Success', detail: response.message, life: 3000 });
      },
      error: (err: HttpErrorResponse) => {
        console.error(err);
        this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Problem updating two factor status', life: 3000 });
      }
    })
  }

  hideShowPass() {
    this.isText = !this.isText;
    this.isText ? this.eyeIcon = "fa-eye" : this.eyeIcon = "fa-eye-slash";
    this.isText ? this.type = "text" : this.type = "password";
  }
}
