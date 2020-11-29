import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable, of} from 'rxjs';
import {Config} from '../../config';
import {map} from 'rxjs/operators';
import {DnsServers} from '../models/entities/servers/dns-servers';
import {CreateOrUpdateDnsServerData} from '../models/data/servers/create-or-update-dns-server-data';
import {DnsServer} from '../models/entities/servers/dns-server';

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

  saveDnsServer(data: CreateOrUpdateDnsServerData): Observable<DnsServer> {
    if (data.id) {
      const url = `${this.baseUrl}/${data.id}`;
      return this.http
        .put<DnsServer>(url, data)
        .pipe(map(result => new DnsServer(result)));
    } else {
      return this.http
        .post<DnsServer>(this.baseUrl, data)
        .pipe(map(result => new DnsServer(result)));
    }
  }

  deleteDnsServer(id: string): Observable<boolean> {
    const url = `${this.baseUrl}/${id}`;
    return this.http
      .delete<boolean>(url);
  }
}
