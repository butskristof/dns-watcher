import { Injectable } from '@angular/core';
import {Observable, of, throwError} from 'rxjs';
import {CredentialsService} from './credentials.service';
import {TokenInfo} from '../models/entities/token-info';
import {tap} from 'rxjs/operators';
import {ExternalAuthService} from './external-auth.service';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(
    private readonly credentialsService: CredentialsService,
    private readonly externalAuthService: ExternalAuthService
  ) { }

  isAuthenticated(): boolean {
    return this.credentialsService.isAuthenticated();
  }

  get token(): string | null {
    return this.credentialsService.token;
  }

  login(username: string, password: string, remember?: boolean): Observable<TokenInfo> {
    return this.externalAuthService
      .authenticate(username, password)
      .pipe(
        tap(data => this.credentialsService.setCredentials(data, remember))
      );
  }

  logout(): Observable<boolean> {
    this.credentialsService.setCredentials();
    return of(true);
  }

  tryRefreshToken(): Observable<TokenInfo> {
    const refreshToken = this.refreshToken;
    if (refreshToken == null) {
      return throwError('failedToRefreshToken');
    }

    return this.externalAuthService
      .refreshToken(refreshToken)
      .pipe(
        tap(tokens => this.credentialsService.setCredentials(tokens, false))
      );
  }

  private get refreshToken(): string | null {
    return this.credentialsService.refreshToken;
  }
}
