import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse
} from '@angular/common/http';
import { Observable, catchError, throwError } from 'rxjs';
import { Router } from '@angular/router';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {


  constructor(private router: Router) { }

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    return next.handle(request)
      .pipe(
        catchError((error: HttpErrorResponse) => {
          let errorMessage = this.handleError(error);
          return throwError(() => new Error(errorMessage));
        })
      );
  }

  private handleError = (error: HttpErrorResponse): string => {
    if (error.status === 404) {
      return this.handleNotFound(error);
    }
    else if (error.status === 400) {
      return this.handleBadRequest(error);
    }
    else if (error.status === 409) {
      return this.handleConfilct(error);
    }
    else if (error.status === 401) {
      return this.handleUnauthorized(error);
    }
    else return error.message;
  }

  private handleNotFound = (error: HttpErrorResponse): string => {
    //this.router.navigate(['/404']);
    return error.error;
  }

  private handleConfilct = (error: HttpErrorResponse): string => {
    throw error.error;
  }
  private handleUnauthorized = (error: HttpErrorResponse): string => {
    throw error.error;
  }

  private handleBadRequest = (error: HttpErrorResponse): string => {
    throw error.error;
  };

}
