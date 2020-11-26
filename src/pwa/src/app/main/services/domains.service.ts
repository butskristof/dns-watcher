import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {Domains} from '../models/entities/domains';
import {Config} from '../../config';
import {map} from 'rxjs/operators';
import {Domain} from '../models/entities/domain';

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

  getDomain(id: string): Observable<Domain> {
    const url = `${this.baseUrl}/${id}`;
    return this.http
      .get<Domain>(url)
      .pipe(map(e => new Domain(e)));
  }
}
