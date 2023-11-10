import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject, map, of, ReplaySubject } from 'rxjs';
import { environment } from 'src/environments/environment';
import { User } from '../shared/models/user';
import { ApiResponse } from '../shared/models/ApiResponse';
import { ForgotPasswordDto } from '../shared/models/ForgotPasswordDto';
import { ResetPasswordComponent } from './reset-password/reset-password.component';
import { ResetPasswordDto } from '../shared/models/ResetPasswordDto';
import { UserForRegistrationDto } from '../shared/models/UserForRegistraionDto';
import { EmailConfirmationDto } from '../shared/models/EmailConfirmationDto';
import { CustomEncoder } from '../shared/custom-encoder';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  apiUrl = environment.apiUrl;
  private currentUserSource = new ReplaySubject<User | null>(1);
  currentUser$ = this.currentUserSource.asObservable();

  isLoggedIn$ = new BehaviorSubject<boolean>(false);

  constructor(private http: HttpClient, private router: Router) { }

  // loadCurrentUser(token: string | null) {
  //   if (token == null) {
  //     this.currentUserSource.next(null);
  //     return of(null);
  //   }

  //   let headers = new HttpHeaders();
  //   headers = headers.set('Authorization', `Bearer ${token}`);

  //   return this.http.get<User>(this.apiUrl + 'Account', { headers }).pipe(
  //     map(user => {
  //       if (user) {
  //         localStorage.setItem('token', user.token);
  //         this.currentUserSource.next(user);
  //         return user;
  //       } else {
  //         return null;
  //       }
  //     })
  //   )
  // }

  login(values: any) {
    return this.http.post<ApiResponse<User>>(this.apiUrl + 'Account/login', values).pipe(
      map(response => {
        localStorage.setItem('token', response.value.token);
        this.isLoggedIn$.next(true);
        this.currentUserSource.next(response.value);
      })
    )
  }

  register(values: UserForRegistrationDto) {
    return this.http.post<any>(this.apiUrl + 'Account/register', values).pipe(
      map(user => {

      })
    )
  }

  logout() {
    localStorage.removeItem('token');
    this.isLoggedIn$.next(false);
    this.currentUserSource.next(null);
    this.router.navigateByUrl('/');
  }

  isLoggedIn() {
    return !!localStorage.getItem('token');
  }

  forgotPassword(values: ForgotPasswordDto) {
    return this.http.post(this.apiUrl + 'Account/forgotPassword', values);
  }

  resetPassword(values: ResetPasswordDto) {
    return this.http.post(this.apiUrl + 'Account/resetPassword', values);
  }

  confirmEmail(values: EmailConfirmationDto) {
    let params = new HttpParams({ encoder: new CustomEncoder() })
    params = params.append('token', values.token);
    params = params.append('email', values.email);

    return this.http.get(this.apiUrl + 'Account/emailConfirmation', { params: params })
  }
}
