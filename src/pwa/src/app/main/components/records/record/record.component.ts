import {Component, Input, OnChanges, OnInit, SimpleChanges} from '@angular/core';
import {Record} from '../../../models/entities/domains/record';
import {RecordType} from '../../../models/entities/domains/record-type';
import {RecordsService} from '../../../services/records.service';
import {Domain} from '../../../models/entities/domains/domain';
import utilities from '../../../../shared/helpers/utilities';
import {DomainsService} from '../../../services/domains.service';
import {Result} from '../../../models/entities/domains/result';

@Component({
  selector: 'app-record',
  templateUrl: './record.component.html',
  styleUrls: ['./record.component.scss']
})
export class RecordComponent
  implements OnInit, OnChanges
{
  @Input()
  domainId: string | null = null;
  @Input()
  recordId: string | null = null;

  record: Record | null = null;
  domain: Domain | null = null;
  recordType = RecordType;

  constructor(
    private readonly recordsService: RecordsService,
    private readonly domainsService: DomainsService,
  ) { }

  ngOnInit(): void {
  }

  ngOnChanges(changes: SimpleChanges): void {
    if ('domainId' in changes || 'recordId' in changes) {
      this.loadRecord();
    }
  }

  // region fetch data
  private loadRecord(): void {
    if (this.domainId == null || this.recordId == null) {
      this.record = null;
      return;
    }

    this.domainsService
      .getDomain(this.domainId)
      .subscribe(result => this.domain = result);
    this.recordsService
      .getRecord(this.domainId, this.recordId)
      .subscribe(result => this.record = result);
  }
  // endregion

  // region getters
  get stringified(): string {
    return this.record == null
      ? ''
      : JSON.stringify(this.record, null, 2);
  }

  getHostname(): string {
    if (this.record == null) {
      return '';
    }

    return utilities.isNullOrWhitespace(this.record.prefix)
      ? (this.domain?.domainName ?? '')
      : `${this.record.prefix}.${this.domain?.domainName}`;
  }

  matches(result: Result): boolean {
    return result.value === this.record?.expectedValue
      && result.timeToLive === this.record?.expectedTimeToLive;
  }

  // endregion
}
