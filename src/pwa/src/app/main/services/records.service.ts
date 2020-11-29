import { Injectable } from '@angular/core';
import {Observable} from 'rxjs';
import {map} from 'rxjs/operators';
import {HttpClient} from '@angular/common/http';
import {Config} from '../../config';
import {Record} from '../models/entities/domains/record';
import {CreateOrUpdateRecordData} from '../models/data/domains/create-or-update-record-data';

@Injectable({
  providedIn: 'root'
})
export class RecordsService {
  private readonly baseUrl = `${Config.apiUrl}/domains`;

  constructor(
    private readonly http: HttpClient
  ) { }

  getRecord(domainId: string, recordId: string): Observable<Record> {
    const url = `${this.baseUrl}/${domainId}/records/${recordId}`;
    return this.http
      .get<Record>(url)
      .pipe(map(e => new Record(e)));
  }

  saveRecord(domainId: string, data: CreateOrUpdateRecordData)
    : Observable<Record> {
    let url = `${this.baseUrl}/${domainId}/records`;
    if (data.id) {
      url = `${url}/${data.id}`;
      return this.http
        .put<Record>(url, data)
        .pipe(map(e => new Record(e)));
    } else {
      return this.http
        .post<Record>(url, data)
        .pipe(map(e => new Record(e)));
    }
  }

  updateResults(domainId: string, recordId: string): Observable<Record> {
    const url = `${this.baseUrl}/${domainId}/records/${recordId}/update`;
    return this.http
      .post<Record>(url, {})
      .pipe(map(e => new Record(e)));
  }
}
