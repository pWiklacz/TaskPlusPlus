import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AccountService } from '../account.service';
import { ForgotPasswordDto } from 'src/app/shared/models/account/ForgotPasswordDto';
import { HttpErrorResponse } from '@angular/common/http';
import { ToastModule } from 'primeng/toast';
import { MessageService } from 'primeng/api';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrls: ['./forgot-password.component.scss'],
})
export class ForgotPasswordComponent {
  clientUrl = environment.clientUrl;
  public errorMessage?: string;
  submitted = false;
  forgotPasswordForm = new FormGroup({
    email: new FormControl('', [Validators.required, Validators.email])
  })

  constructor(private accountService: AccountService,
     private messageService: MessageService){}

  get form() { return this.forgotPasswordForm.controls; }

  onSubmit(forgotPasswordFormValue: string) {
    this.submitted = true;
   
    if (this.forgotPasswordForm.invalid) {
        return;
    }
    
    const forgotPassDto: ForgotPasswordDto = {
      email: forgotPasswordFormValue,
      clientUri: this.clientUrl + 'account/resetpassword'
    }
   
    this.accountService.forgotPassword(forgotPassDto).subscribe({
      next: (response : any) => {
        this.messageService.add({ severity: 'success', summary: 'Success', detail: response.message, life: 10000 });
      },
      error: (err: HttpErrorResponse) => {
        this.errorMessage = err.message}
    })
  }
}
