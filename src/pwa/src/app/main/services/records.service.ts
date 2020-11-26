import { Injectable } from '@angular/core';
import {Observable} from 'rxjs';
import {map} from 'rxjs/operators';
import {HttpClient} from '@angular/common/http';
import {Config} from '../../config';
import {Record} from '../models/entities/domains/record';

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
}
