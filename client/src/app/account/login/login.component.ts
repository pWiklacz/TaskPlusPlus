import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { AccountService } from '../account.service';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';
import { MessageService } from 'primeng/api';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'], 
  providers: [MessageService]
})
export class LoginComponent {
  public errorMessage?: string;
  type: string = "password";
  isText: boolean = false;
  eyeIcon: string = "fa-eye-slash";
  loginForm = new FormGroup({
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', Validators.required)
  })
  returnUrl: string;
  submitted = false;

  constructor(private accountService: AccountService, private router: Router,
    private activatedRoute: ActivatedRoute,
    private messageService: MessageService) {
    this.returnUrl = this.activatedRoute.snapshot.queryParams['returnUrl'] || '/'
  }

  get form() { return this.loginForm.controls; }

  onSubmit() {
    this.submitted = true;
 
    if (this.loginForm.invalid) {
        return;
    }
    
    this.accountService.login(this.loginForm.value).subscribe({
      next: () => this.router.navigateByUrl('dashboard'),
      error: (err: HttpErrorResponse) => {
        console.log(err);
        this.errorMessage = err.message}
    })
  }

  externalLogin = () => {
   this.accountService.signInWithFB();
  }

  hideShowPass() {
    this.isText = !this.isText;
    this.isText ? this.eyeIcon = "fa-eye" : this.eyeIcon = "fa-eye-slash";
    this.isText ? this.type = "text" : this.type = "password";
  }
}
