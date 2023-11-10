import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MustMatch } from 'src/app/shared/validators/passwords-must-match-validator';
import { AccountService } from '../account.service';
import { ActivatedRoute, Router } from '@angular/router';
import { ResetPasswordDto } from 'src/app/shared/models/ResetPasswordDto';
import { MessageService } from 'primeng/api';

@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrls: ['./reset-password.component.scss'],
})
export class ResetPasswordComponent implements OnInit {
  errors: string[] | null = null;
  submitted = false;
  type: string = "password";
  isText: boolean = false;
  eyeIcon: string = "fa-eye-slash"
  complexPassword = '(?=^.{6,10}$)(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&*()_+{}":;\'?/>.<,])(?!.*\\s).*$';
  private token?: string;
  private email?: string;

  resetPasswordForm = new FormGroup({
    password: new FormControl('', [Validators.required, Validators.pattern(this.complexPassword)]),
    confirmPassword: new FormControl('', Validators.required)
  }, {
    validators: MustMatch('password', 'confirmPassword')
  });

  constructor(private fb: FormBuilder,
    private accountService: AccountService,
    private router: Router,
    private route: ActivatedRoute,
    private messageService: MessageService) { }

  ngOnInit(): void {
    this.token = this.route.snapshot.queryParams['token'];
    this.email = this.route.snapshot.queryParams['email'];
  }

  get form() { return this.resetPasswordForm.controls; }

  onSubmit() {
    this.submitted = true;

    if (this.resetPasswordForm.invalid) {
      return;
    }
    const resetPass = this.resetPasswordForm.value;

    const resetPassDto: ResetPasswordDto = {
      password: resetPass.password!,
      confirmPassword: resetPass.confirmPassword!,
      token: this.token!,
      email: this.email!
    }

    this.errors = [];
    this.accountService.resetPassword(resetPassDto).subscribe({
      next: (response: any) => {
        this.router.navigateByUrl('/account/login').then(()=>{
          this.messageService.add({ severity: 'success', summary: response.message, life: 10000});
        })  
      },
      error: error => {
        if (error.value) this.errors = error.value
        else this.errors?.push(error.message)
      }
    })
  }

  hideShowPass() {
    this.isText = !this.isText;
    this.isText ? this.eyeIcon = "fa-eye" : this.eyeIcon = "fa-eye-slash";
    this.isText ? this.type = "text" : this.type = "password";
  }
}

