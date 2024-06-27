
import { HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpInterceptorFn, HttpRequest } from '@angular/common/http';
import { Observable, catchError, switchMap, throwError } from 'rxjs';
import { AuthServiceService } from './auth-service.service';
import { Injectable, inject } from '@angular/core';

export const TokenInterceptor: HttpInterceptorFn = (request, next) => {
  const authService = inject(AuthServiceService);
  const token = authService.currentAccessToken;
  if (token) {
    request = request.clone({
      setHeaders: {
        Authorization: `Bearer ${token}`
      }
    });
  }
  return next(request).pipe(
    catchError((error: HttpErrorResponse) => {
      if (error.status === 401) {
        return authService.refreshTokens().pipe(
          switchMap(() => {
            request = request.clone({
              setHeaders: {
                Authorization: `Bearer ${authService.currentAccessToken}`
              }
            });
            return next(request);
          }),
          catchError(err => {
            // Add any additional logic to handle logout
            return throwError(() => err);
          })
        );
      } else {
        return throwError(() => error);
      }
    })
  );
};

