import {Component, Input, OnChanges, OnInit, SimpleChanges} from '@angular/core';
import {Record} from '../../../models/entities/domains/record';
import {RecordType} from '../../../models/entities/domains/record-type';
import {RecordsService} from '../../../services/records.service';
import {Domain} from '../../../models/entities/domains/domain';
import utilities from '../../../../shared/helpers/utilities';
import {DomainsService} from '../../../services/domains.service';
import {Result} from '../../../models/entities/domains/result';
import {Config} from '../../../../config';
import {ActionButtonStyle} from '../../../../shared/models/viewmodels/action-button-style';
import {DialogService} from '../../../../dialog/services/dialog.service';
import {NotifierService} from '../../../../shared/services/notifier.service';
import {EditRecordComponent} from '../edit-record/edit-record.component';
import {NavigationService} from '../../../../shared/services/navigation.service';

@Component({
  selector: 'app-record',
  templateUrl: './record.component.html',
  styleUrls: ['./record.component.scss']
})
export class RecordComponent
  implements OnInit, OnChanges {
  @Input()
  domainId: string | null = null;
  @Input()
  recordId: string | null | undefined = null;

  actionButtonStyles = ActionButtonStyle;

  record?: Record;
  domain?: Domain;
  recordType = RecordType;

  updating = false;

  constructor(
    private readonly recordsService: RecordsService,
    private readonly domainsService: DomainsService,
    private readonly dialogService: DialogService,
    private readonly notifier: NotifierService,
    private readonly navigationService: NavigationService
  ) {
  }

  ngOnInit(): void {
  }

  ngOnChanges(changes: SimpleChanges): void {
    if ('domainId' in changes || 'recordId' in changes) {
      this.loadRecord();
    }
  }

  // region actions

  edit(): void {
    const ref = this.dialogService
      .open(EditRecordComponent, {
        data: {
          domainId: this.domain?.id,
          domainName: this.domain?.domainName,
          record: this.record
        }
      });
    ref.afterClosed
      .subscribe(result => {
        if (result) {
          this.loadRecord();
        }
      }, error => this.notifier.showErrorToast(error));
  }

  promptDelete(): void {
    const ref = this.dialogService
      .confirm('records.delete.message',
        this.getHostname(),
        'danger');

    ref.afterClosed
      .subscribe(result => {
        if (result === true) {
          this.deleteRecord();
        }
      });
  }

  updateResults(): void {
    if (this.domainId == null || this.recordId == null) {
      this.record = undefined;
      return;
    }

    this.updating = true;
    this.recordsService
      .updateResults(this.domainId, this.recordId)
      .subscribe(() => this.loadRecord())
      .add(() => this.updating = false);
  }

  private deleteRecord(): void {
    if (this.record?.id == null || this.domainId == null) {
      return;
    }

    this.recordsService
      .deleteRecord(this.domainId, this.record.id)
      .subscribe(result => {
        this.notifier
          .showSuccessToast('records.delete.deleted', true);
        this.navigationService
          .goToUrl(this.navigationService.getDomainDetailsLink(this.domainId ?? ''));
      }, error => this.notifier
        .showErrorToast(error));
  }

  // endregion

  // region fetch data
  private loadRecord(): void {
    if (this.domainId == null || this.recordId == null) {
      this.record = undefined;
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
  getHostname(): string {
    if (this.record == null) {
      return '';
    }

    return utilities.isNullOrWhitespace(this.record.prefix)
      ? (this.domain?.domainName ?? '')
      : `${this.record.prefix}.${this.domain?.domainName}`;
  }

  valueClass(result: Result): string {
    return this.valueMatches(result) ? 'ok' : 'nok';
  }

  ttlClass(result: Result): string {
    return this.ttlMatches(result) ? 'ok' : 'nok';
  }

  indicatorClass(result: Result): string {
    return 'indicator ' + (this.valueMatches(result) && this.ttlMatches(result)
      ? 'ok' : 'nok');
  }

  private valueMatches(result: Result): boolean {
    return result.value === this.record?.expectedValue;
  }

  private ttlMatches(result: Result): boolean {
    return result.timeToLive !== undefined
      && result.timeToLive > 0
      && result.timeToLive <= (this.record?.expectedTimeToLive ?? -1);
  }

  get dateFormat(): string {
    return Config.defaultDateFormat + ':ss';
  }

  // endregion
}
