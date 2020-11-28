import {Component, Input, OnChanges, OnInit, SimpleChanges} from '@angular/core';
import {Record} from '../../../models/entities/domains/record';
import {RecordType} from '../../../models/entities/domains/record-type';
import {RecordsService} from '../../../services/records.service';
import {Domain} from '../../../models/entities/domains/domain';
import utilities from '../../../../shared/helpers/utilities';
import {DomainsService} from '../../../services/domains.service';
import {Result} from '../../../models/entities/domains/result';
import {Config} from '../../../../config';

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
  recordId: string | null | undefined = null;

  record: Record | null = null;
  domain: Domain | null = null;
  recordType = RecordType;

  updating = false;

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

  // region actions
  updateResults(): void {
    if (this.domainId == null || this.recordId == null) {
      this.record = null;
      return;
    }

    this.updating = true;
    this.recordsService
      .updateResults(this.domainId, this.recordId)
      .subscribe(() => this.loadRecord())
      .add(() => this.updating = false);
  }
  // endregion

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

  matchesValue(result: Result): boolean {
    return result.value === this.record?.expectedValue;
  }

  matchesTtl(result: Result): boolean {
    if (result?.timeToLive == null) {
      return false;
    }

    return result.timeToLive > 0 && result.timeToLive <= (this.record?.expectedTimeToLive ?? -1);
  }

  get dateFormat(): string {
    return Config.defaultDateFormat + ':ss';
  }

  // endregion
}
