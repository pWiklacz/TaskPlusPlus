import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AccountService } from '../account.service';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-email-confirmation',
  templateUrl: './email-confirmation.component.html',
  styleUrls: ['./email-confirmation.component.scss']
})
export class EmailConfirmationComponent implements OnInit {
  showSuccess?: boolean;
  showError?: boolean;
  errorMessage?: string;
  

  ngOnInit(): void {
    this.confirmEmail();
  }

  constructor(private _authService: AccountService, private _route: ActivatedRoute) { }

  private confirmEmail = () => {
    this.showError = this.showSuccess = false;

    const _token = this._route.snapshot.queryParams['token'];
    const _email = this._route.snapshot.queryParams['email'];

    const EmailConfirmationDto = {
      token: _token,
      email: _email
    }
    
    this._authService.confirmEmail(EmailConfirmationDto)
    .subscribe({
      next: (_) => this.showSuccess = true,
      error: (err: HttpErrorResponse) => {
        this.showError = true;
        this.errorMessage = err.message;
      }
    })
  }
}
