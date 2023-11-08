import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject, map, of, ReplaySubject } from 'rxjs';
import { environment } from 'src/environments/environment';
import { User } from '../shared/models/user';
import { ApiResponse } from '../shared/models/ApiResponse';

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

  register(values: any) {
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
}
