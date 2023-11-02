import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { AccountService } from '../account.service';
import { Router } from '@angular/router';
import { MustMatch } from 'src/app/shared/validators/passwords-must-match-validator';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent {
  errors: string[] | null = null;
  submitted = false;
  type: string = "password";
  isText: boolean = false;
  eyeIcon: string = "fa-eye-slash"
  complexPassword = '(?=^.{6,10}$)(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&*()_+{}":;\'?/>.<,])(?!.*\\s).*$';

  registerForm = new FormGroup({
    firstName: new FormControl('', Validators.required),
    lastName: new FormControl('', Validators.required),
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', [Validators.required, Validators.pattern(this.complexPassword)]),
    confirmPassword: new FormControl('', Validators.required)
  }, {
    validators: MustMatch('password', 'confirmPassword')
  });

  constructor(private fb: FormBuilder, private accountService: AccountService, private router: Router) { }

  get form() { return this.registerForm.controls; }

  onSubmit() {
    // this.submitted = true;

    // if (this.registerForm.invalid) {
    //   return;
    // }

    this.accountService.register(this.registerForm.value).subscribe({
      next: () => this.router.navigateByUrl('/dashboard'),
      error: error => this.errors = error.errors
    })
  }

  hideShowPass() {
    this.isText = !this.isText;
    this.isText ? this.eyeIcon = "fa-eye" : this.eyeIcon = "fa-eye-slash";
    this.isText ? this.type = "text" : this.type = "password";
  }
}


