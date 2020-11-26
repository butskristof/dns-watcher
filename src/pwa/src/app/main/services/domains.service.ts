import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {Domains} from '../models/entities/domains';
import {Config} from '../../config';
import {map} from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class DomainsService {
  private readonly baseUrl = `${Config.apiUrl}/domains`;

  constructor(
    private readonly http: HttpClient
  ) {}

  getDomains(): Observable<Domains> {
    return this.http
      .get<Domains>(this.baseUrl)
      .pipe(map(e => new Domains(e)));
  }
}
