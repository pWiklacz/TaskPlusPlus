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
import { TwoFactorDto } from '../shared/models/account/TwoFactorDto';
import { changeTwoFactorEnabledStatusDto } from '../shared/models/account/changeTwoFactorEnabledStatusDto';


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
      console.log(this.userPayload)
      this.getDataFromToken();
    }
  }

  login(values: any) {
    this.busyService.busy();
    return this.http.post<ApiResponse<User>>(this.apiUrl + 'Account/login', values).pipe(
      map(response => {
        if (response.value.is2StepVerificationRequired) {
          this.isExternalAuth$.next(false);
          this.router.navigate(['/authentication/twostepverification'],
            { queryParams: { provider: response.value.provider, email: response.value.email } })
        }
        else {
          const userSettingsJson = JSON.stringify(response.value.settings);
          localStorage.setItem('settings', userSettingsJson);
          localStorage.setItem('token', response.value.token);
          localStorage.setItem("refreshToken", response.value.refreshToken);
          this.themeService.current = response.value.settings.theme;
          this.userPayload = this.DecodeToken();
          this.getDataFromToken();
          this.isLoggedIn$.next(true);
          this.isExternalAuth$.next(false);
          this.router.navigate([`app/dashboard/${response.value.settings.startPage.toLowerCase()}`]);
        }
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
    localStorage.removeItem("refreshToken");
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

  updateProfileInformation(values: UpdateProfileDto) {
    return this.http.put<ApiResponse<User>>(this.apiUrl + 'Account/updateData', values).pipe(
      map(response => {
        localStorage.setItem('token', response.value.token);
        this.userPayload = this.DecodeToken();
        this.getDataFromToken();
        return response
      }),
    );;
  }

  updateEmail(values: UpdateEmailDto) {
    return this.http.put<ApiResponse<any>>(this.apiUrl + 'Account/updateEmail', values);
  }

  updatePassword(values: UpdatePasswordDto) {
    return this.http.put<ApiResponse<any>>(this.apiUrl + 'Account/updatePassword', values);
  }

  addPassword(values: AddPasswordDto) {
    return this.http.post<ApiResponse<User>>(this.apiUrl + 'Account/addPassword', values).pipe(
      map(response => {
        localStorage.setItem('token', response.value.token);
        this.userPayload = this.DecodeToken();
        this.getDataFromToken();
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

  public twoStepLogin = (values: TwoFactorDto) => {
    this.busyService.busy();
    return this.http.post<ApiResponse<User>>(this.apiUrl + 'Account/twoStepVerification', values).pipe(
      map(response => {
        const userSettingsJson = JSON.stringify(response.value.settings);
        localStorage.setItem('settings', userSettingsJson);
        localStorage.setItem('token', response.value.token);
        this.themeService.current = response.value.settings.theme;
        this.userPayload = this.DecodeToken();
        this.getDataFromToken();
        this.isLoggedIn$.next(true);
        this.router.navigate([`app/dashboard/${response.value.settings.startPage.toLowerCase()}`]);
      }),
      finalize(() => this.busyService.idle()));
  }

  changeTwoFactorEnabledStatus(values: changeTwoFactorEnabledStatusDto) {
    return this.http.put<ApiResponse<string>>(this.apiUrl + 'Account/changeTwoFactorEnabledStatus', values).pipe(
      map(response => {
        localStorage.setItem('token', response.value);
        this.userPayload = this.DecodeToken();
        this.getDataFromToken();
        return response
      }),
    );
  }

  private validateExternalAuth(externalAuth: ExternalAuthDto) {
    this.externalLogin(externalAuth)
      .subscribe({
        next: (res) => {
          if (res.value.is2StepVerificationRequired) {
            this.isExternalAuth$.next(true);
            this.router.navigate(['/account/twostepverification'],
              { queryParams: { provider: res.value.provider, email: res.value.email } })
          }
          else {
            localStorage.setItem("token", res.value.token);
            const userSettingsJson = JSON.stringify(res.value.settings);
            localStorage.setItem('settings', userSettingsJson);
            this.themeService.current = res.value.settings.theme;
            this.isLoggedIn$.next(true);
            this.isExternalAuth$.next(true);
            this.userPayload = this.DecodeToken();
            this.getDataFromToken();
            this.router.navigate([`app/dashboard/${res.value.settings.startPage.toLowerCase()}`]);
          }
        },
        error: (err: HttpErrorResponse) => {
          this.signOutExternal();
        }
      });
  }

  public isTokenExpired(): boolean {

    this.userPayload = this.DecodeToken();

    if (this.userPayload && this.userPayload.exp) {

      const expireDateInSeconds = this.userPayload.exp;

      const expireDate = new Date(expireDateInSeconds * 1000);
      console.log(expireDate)

      const currentDate = new Date();

      return expireDate < currentDate;
    } else {

      return true;
    }
  }

  public async tryRefreshingTokens(token: string): Promise<boolean> 
  {
    const refreshToken = localStorage.getItem("refreshToken");
    if (!token || !refreshToken) { 
      return false;
    }

    const credentials = JSON.stringify({ accessToken: token, refreshToken: refreshToken });
    let isRefreshSuccess: boolean;

    const refreshRes = await new Promise<ApiResponse<User>>((resolve, reject) => {
      this.http.post<ApiResponse<User>>(this.apiUrl + 'Token/refresh', credentials, {
        headers: new HttpHeaders({
          "Content-Type": "application/json"
        })
      }).subscribe({
        next: (res) => resolve(res),
        error: (_) => { reject; isRefreshSuccess = false;}
      });
    });

    localStorage.setItem("token", refreshRes.value.token);
    localStorage.setItem("refreshToken", refreshRes.value.refreshToken);
    isRefreshSuccess = true;
    return isRefreshSuccess;
  }

  private DecodeToken() {
    const token = localStorage.getItem('token')!;
    return jwtDecode(token);
  }

  private getDataFromToken() {
    this.userStoreService.setFirstName(this.getFirstNameFromToken());
    this.userStoreService.setLastName(this.getLastNameFromToken());
    this.userStoreService.setEmail(this.getEmailFromToken());
    this.userStoreService.setUId(this.getUserIdFromToken());
    this.userStoreService.setHasPassword(this.getHasPasswordFromToken());
    this.userStoreService.setTwoFactorEnabled(this.getTwoFactorEnabledFromToken());
  }

  private getFirstNameFromToken() {
    if (this.userPayload)
      return this.userPayload.name;
  }

  private getLastNameFromToken() {
    if (this.userPayload)
      return this.userPayload.family_name;
  }

  private getEmailFromToken() {
    if (this.userPayload)
      return this.userPayload.email;
  }

  private getUserIdFromToken() {
    if (this.userPayload)
      return this.userPayload.uid;
  }

  private getHasPasswordFromToken() {
    if (this.userPayload)
      return this.userPayload.hasPassword;
  }

  private getTwoFactorEnabledFromToken() {
    if (this.userPayload)
      return this.userPayload.twoFactorEnabled;
  }

  getUserSettings(): UserSettings | null {
    const userSettingsJson = localStorage.getItem('settings');

    if (userSettingsJson) {
      return JSON.parse(userSettingsJson);
    }
    return null;
  }

  private createCompleteRoute = (route: string, envAddress: string) => {
    return `${envAddress}/${route}`;
  }
}