import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {Domains} from '../models/entities/domains/domains';
import {Config} from '../../config';
import {map} from 'rxjs/operators';
import {Domain} from '../models/entities/domains/domain';
import {CreateOrUpdateDomainData} from '../models/data/domains/create-or-update-domain-data';
import {DnsServer} from '../models/entities/servers/dns-server';

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

  saveDomain(data: CreateOrUpdateDomainData): Observable<Domain> {
    if (data.id) {
      const url = `${this.baseUrl}/${data.id}`;
      return this.http
        .put<Domain>(url, data)
        .pipe(map(result => new Domain(result)));
    } else {
      return this.http
        .post<Domain>(this.baseUrl, data)
        .pipe(map(result => new Domain(result)));
    }
  }
}
