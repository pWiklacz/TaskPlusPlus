import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AccountService } from '../account.service';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';
import { TwoFactorDto } from 'src/app/shared/models/account/TwoFactorDto';

@Component({
  selector: 'app-two-step-verification',
  templateUrl: './two-step-verification.component.html',
  styleUrls: ['./two-step-verification.component.scss']
})
export class TwoStepVerificationComponent implements OnInit {
  private provider?: string;
  private email?: string;
  submitted = false;

  twoStepForm = new FormGroup({
    twoFactorCode: new FormControl('', [Validators.required]),
  });

  showError?: boolean;
  errorMessage?: string;

  constructor(private authService: AccountService, private route: ActivatedRoute,
    private router: Router) { }

  ngOnInit(): void {

    this.provider = this.route.snapshot.queryParams['provider'];
    this.email = this.route.snapshot.queryParams['email'];
  }

  validateControl = (controlName: string) => {
    return this.twoStepForm!.get(controlName)!.invalid && this.twoStepForm!.get(controlName)!.touched
  }

  hasError = (controlName: string, errorName: string) => {
    return this.twoStepForm!.get(controlName)!.hasError(errorName)
  }

  get form() { return this.twoStepForm.controls; }

  loginUser = (twoStepFromValue: any) => {
    this.showError = false;
    this.submitted = true;
    const formValue = { ...twoStepFromValue };
    let twoFactorDto: TwoFactorDto = {
      email: this.email!,
      provider: this.provider!,
      token: formValue.twoFactorCode
    }
    this.authService.twoStepLogin(twoFactorDto)
      .subscribe({
        error: (err: HttpErrorResponse) => {
          this.errorMessage = err.message;
          this.showError = true;
        }
      })
  }
}
