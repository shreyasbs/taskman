import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable, signal } from '@angular/core';
import { environment } from '../environments/environment';
import { JwtHelperService } from '@auth0/angular-jwt';
import { BehaviorSubject, Observable, catchError, map, switchMap, tap, throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthServiceService {
  api: any = environment.api;
  private jwtHelper = new JwtHelperService();
  private refreshTokenTimeout: any;
  private accessToken = signal<string | null>(null);
  private refreshToken = signal<string | null>(null);

  constructor(private http: HttpClient) {

  }


  private handleError(error: HttpErrorResponse) {
    let errorMessage = '';
    if (error.error instanceof ErrorEvent) {
      errorMessage = `Error: ${error.error.message}`;
    } else {
      errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
    }
    return throwError(() => errorMessage);
  }

  private storeTokens(tokens: any) {
    this.accessToken.set(tokens.accessToken);
    this.refreshToken.set(tokens.refreshToken);
    this.startRefreshTokenTimer();
  }

  public get currentAccessToken(): string | null {
    return this.accessToken();
  }

  public isAuthenticated(): any {
    const token = this.currentAccessToken;
    return token && !this.jwtHelper.isTokenExpired(token);
  }

  refreshTokens(): Observable<any> {
    const refreshToken = this.refreshToken();
    return this.http.post<any>(`${this.api}/auth/refresh-token`, { token: refreshToken })
      .pipe(
        catchError(this.handleError),
        tap(tokens => this.storeTokens(tokens))
      );
  }

  private startRefreshTokenTimer() {
    const accessToken = this.currentAccessToken;
    if (!accessToken) return;

    const expiration = this.jwtHelper.getTokenExpirationDate(accessToken)?.getTime() || 0;
    const timeout = expiration - Date.now() - (60 * 1000); // Refresh 1 minute before expiry
    this.refreshTokenTimeout = setTimeout(() => this.refreshTokens().subscribe(), timeout);
  }

  private stopRefreshTokenTimer() {
    clearTimeout(this.refreshTokenTimeout);
  }

  login(loginObj: any) {
    return this.http.post(this.api + 'auth/login', loginObj).pipe(
      catchError(this.handleError),
      tap(tokens => {
        return this.storeTokens(tokens);
      })
    );
  }

  register(registerObj: any) {
    return this.http.post(this.api + 'auth/register', registerObj);
  }
}
