import { HttpClient, HttpErrorResponse, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject, finalize, map, of, ReplaySubject, Subject } from 'rxjs';
import { environment } from 'src/environments/environment';
import { User, UserSettings } from '../shared/models/account/user';
import { ApiResponse } from '../shared/models/ApiResponse';
import { ForgotPasswordDto } from '../shared/models/account/ForgotPasswordDto';
import { ResetPasswordDto } from '../shared/models/account/ResetPasswordDto';
import { UserForRegistrationDto } from '../shared/models/account/UserForRegistraionDto';
import { EmailConfirmationDto } from '../shared/models/account/EmailConfirmationDto';
import { CustomEncoder } from '../shared/custom-encoder';
import { FacebookLoginProvider, SocialAuthService, SocialUser } from "@abacritt/angularx-social-login";
import { ExternalAuthDto } from '../shared/models/account/ExternalAuthDto';
import { ThemeService } from '../core/services/theme.service';
import { BusyService } from '../core/services/busy.service';
import { jwtDecode } from "jwt-decode";
import { UserStoreService } from './user-store.service';
import { UpdateUserSettingsDto } from '../shared/models/account/UpdateUserSettingsDto';
import { UpdateProfileDto } from '../shared/models/account/UpdateProfileDto';
import { UpdateEmailDto } from '../shared/models/account/updateEmailDto';
import { UpdatePasswordDto } from '../shared/models/account/UpdatePasswordDto';
import { AddPasswordDto } from '../shared/models/account/AddPasswordDto';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  apiUrl = environment.apiUrl;
  isLoggedIn$ = new BehaviorSubject<boolean>(false);
  isExternalAuth$ = new BehaviorSubject<boolean>(false);
  private extAuthChangeSub = new Subject<SocialUser>();
  public extAuthChanged = this.extAuthChangeSub.asObservable();
  private userPayload: any;

  constructor(private http: HttpClient, private router: Router,
    private externalAuthService: SocialAuthService,
    private themeService: ThemeService,
    private busyService: BusyService,
    private userStoreService: UserStoreService) {
    this.externalAuthService.authState.subscribe((user) => {
      if (user) {
        this.extAuthChangeSub.next(user);
        const externalAuth: ExternalAuthDto = {
          email: user.email,
          provider: user.provider,
          accessToken: user.idToken ?? user.authToken,
          firstName: user.firstName,
          lastName: user.lastName ?? ''
        }
        this.validateExternalAuth(externalAuth);
      }
    })
    if (localStorage.getItem('token')) {
      this.userPayload = this.DecodeToken();
      this.userStoreService.setFirstName(this.getFirstNameFromToken());
      this.userStoreService.setLastName(this.getLastNameFromToken());
      this.userStoreService.setEmail(this.getEmailFromToken());
      this.userStoreService.setUId(this.getUserIdFromToken());
      this.userStoreService.setHasPassword(this.getHasPasswordFromToken());
    }
  }

  login(values: any) {
    this.busyService.busy();
    return this.http.post<ApiResponse<User>>(this.apiUrl + 'Account/login', values).pipe(
      map(response => {
        const userSettingsJson = JSON.stringify(response.value.settings);
        localStorage.setItem('settings', userSettingsJson);
        localStorage.setItem('token', response.value.token);
        this.themeService.current = response.value.settings.theme;
        this.userPayload = this.DecodeToken();
        this.userStoreService.setFirstName(this.getFirstNameFromToken());
        this.userStoreService.setLastName(this.getLastNameFromToken());
        this.userStoreService.setEmail(this.getEmailFromToken());
        this.userStoreService.setUId(this.getUserIdFromToken());
        this.userStoreService.setHasPassword(this.getHasPasswordFromToken())
        this.isLoggedIn$.next(true);
        this.isExternalAuth$.next(false);
        this.router.navigate([`app/dashboard/${response.value.settings.startPage.toLowerCase()}`]);
      }),
      finalize(() => this.busyService.idle())
    )
  }

  register(values: UserForRegistrationDto) {
    this.busyService.busy();
    return this.http.post<any>(this.apiUrl + 'Account/register', values).pipe(
      finalize(() => this.busyService.idle())
    )
  }

  logout() {
    this.themeService.current = ThemeService.default;
    this.busyService.busy();
    this.userStoreService.clearSignals();
    localStorage.removeItem('token');
    localStorage.removeItem('settings');
    this.isLoggedIn$.next(false);

    if (this.isExternalAuth$.value) {
      this.signOutExternal();
    }
    this.router.navigateByUrl('/account/login').then(() => this.busyService.idle());
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

  externalLogin(values: ExternalAuthDto) {
    this.busyService.busy();
    return this.http.post<ApiResponse<User>>(this.apiUrl + 'Account/externalLogin', values).pipe(
      finalize(() => this.busyService.idle())
    );
  }

  signInWithFB(): void {
    this.externalAuthService.signIn(FacebookLoginProvider.PROVIDER_ID);
  }

  signOutExternal = () => {
    this.externalAuthService.signOut();
  }

  updateSettings(values: UpdateUserSettingsDto) {
    return this.http.put<ApiResponse<any>>(this.apiUrl + 'Account/updateSettings', values);
  }

  updateProfileInformation(values: UpdateProfileDto)
  {
    return this.http.put<ApiResponse<User>>(this.apiUrl + 'Account/updateData', values).pipe(
      map(response => {
        localStorage.setItem('token', response.value.token);
        this.userPayload = this.DecodeToken();
        this.userStoreService.setHasPassword(this.getHasPasswordFromToken())
        return response
      }),
    );;
  }

  updateEmail(values: UpdateEmailDto)
  {
    return this.http.put<ApiResponse<any>>(this.apiUrl + 'Account/updateEmail', values);
  }

  updatePassword(values: UpdatePasswordDto)
  {
    return this.http.put<ApiResponse<any>>(this.apiUrl + 'Account/updatePassword', values);
  }

  addPassword(values: AddPasswordDto)
  {
    return this.http.post<ApiResponse<User>>(this.apiUrl + 'Account/addPassword', values).pipe(
      map(response => {
        localStorage.setItem('token', response.value.token);
        this.userPayload = this.DecodeToken();
        this.userStoreService.setFirstName(this.getFirstNameFromToken());
        this.userStoreService.setLastName(this.getLastNameFromToken());
        return response
      }),
    );
  }

  confirmChangeEmail(values: EmailConfirmationDto) {
    let params = new HttpParams({ encoder: new CustomEncoder() })
    params = params.append('token', values.token);
    params = params.append('email', values.email);

    return this.http.get(this.apiUrl + 'Account/changeEmailConfirmation', { params: params })
  }


  private validateExternalAuth(externalAuth: ExternalAuthDto) {
    this.externalLogin(externalAuth)
      .subscribe({
        next: (res) => {
          localStorage.setItem("token", res.value.token);
          const userSettingsJson = JSON.stringify(res.value.settings);
          localStorage.setItem('settings', userSettingsJson);
          this.themeService.current = res.value.settings.theme;
          this.isLoggedIn$.next(true);
          this.isExternalAuth$.next(true);
          this.userPayload = this.DecodeToken();
          this.userStoreService.setFirstName(this.getFirstNameFromToken());
          this.userStoreService.setLastName(this.getLastNameFromToken());
          this.userStoreService.setEmail(this.getEmailFromToken());
          this.userStoreService.setUId(this.getUserIdFromToken());
          this.userStoreService.setHasPassword(this.getHasPasswordFromToken())
          this.router.navigate([`app/dashboard/${res.value.settings.startPage.toLowerCase()}`]);
        },
        error: (err: HttpErrorResponse) => {
          this.signOutExternal();
        }
      });
  }

  DecodeToken() {
    const token = localStorage.getItem('token')!;
    return jwtDecode(token);
  }

  getFirstNameFromToken() {
    if (this.userPayload)
      return this.userPayload.name;
  }

  getLastNameFromToken() {
    if (this.userPayload)
      return this.userPayload.family_name;
  }

  getEmailFromToken() {
    if (this.userPayload)
      return this.userPayload.email;
  }

  getUserIdFromToken() {
    if (this.userPayload)
      return this.userPayload.uid;
  }

  getHasPasswordFromToken(){
    if (this.userPayload)
    return this.userPayload.hasPassword;
  }

  getUserSettings(): UserSettings | null {
    const userSettingsJson = localStorage.getItem('settings');

    if (userSettingsJson) {
      return JSON.parse(userSettingsJson);
    }
    return null;
  }
}