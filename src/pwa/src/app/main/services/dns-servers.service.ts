import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {Config} from '../../config';
import {map} from 'rxjs/operators';
import {DnsServers} from '../models/entities/servers/dns-servers';

@Injectable({
  providedIn: 'root'
})
export class DnsServersService {
  private readonly baseUrl = `${Config.apiUrl}/dnsservers`;

  constructor(
    private readonly http: HttpClient
  ) { }

  getDnsServers(): Observable<DnsServers>{
    return this.http
      .get<DnsServers>(this.baseUrl)
      .pipe(map(e => new DnsServers(e)));
  }

  deleteDnsServer(id: string): Observable<boolean> {
    const url = `${this.baseUrl}/${id}`;
    return this.http
      .delete<boolean>(url);
  }
}
