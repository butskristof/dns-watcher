import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {Config} from '../../config';
import {Observable} from 'rxjs';
import {TokenInfo} from '../models/entities/token-info';
import {LoginData} from '../models/data/login-data';
import {map} from 'rxjs/operators';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable({
  providedIn: 'root'
})
export class ExternalAuthService {
  constructor(
    private readonly http: HttpClient
  ) { }

  authenticate(username: string, password: string): Observable<TokenInfo> {
    const url = `${Config.apiUrl}/auth/authenticate`;
    console.log(url);
    const body = new LoginData(username, password);
    return this.http
      .post<TokenInfo>(url, body, httpOptions)
      .pipe(map(e => new TokenInfo(e)));
  }
}