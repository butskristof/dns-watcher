import {Component, Input, OnChanges, OnInit, SimpleChanges} from '@angular/core';
import {Domain} from '../../../models/entities/domains/domain';
import {DomainsService} from '../../../services/domains.service';
import {Record} from '../../../models/entities/domains/record';
import utilities from '../../../../shared/helpers/utilities';
import {RecordType} from '../../../models/entities/domains/record-type';
import {NavigationService} from '../../../../shared/services/navigation.service';
import {ActionButtonStyle} from '../../../../shared/models/viewmodels/action-button-style';
import {EditDomainComponent} from '../edit-domain/edit-domain.component';
import {DialogService} from '../../../../dialog/services/dialog.service';
import {NotifierService} from '../../../../shared/services/notifier.service';
import {EditRecordComponent} from '../../records/edit-record/edit-record.component';

@Component({
  selector: 'app-domain',
  templateUrl: './domain.component.html',
  styleUrls: ['./domain.component.scss']
})
export class DomainComponent
  implements OnInit, OnChanges {
  @Input()
  domainId: string | null = null;

  domain: Domain | null = null;

  recordType = RecordType;
  actionButtonStyles = ActionButtonStyle;

  constructor(
    private readonly domainsService: DomainsService,
    private readonly navigationService: NavigationService,
    private readonly dialogService: DialogService,
    private readonly notifier: NotifierService
  ) {
  }

  ngOnInit(): void {
  }

  ngOnChanges(changes: SimpleChanges): void {
    if ('domainId' in changes) {
      this.loadDomain();
    }
  }

  // region fetch data

  private loadDomain(): void {
    if (this.domainId == null) {
      this.domain = null;
      return;
    }

    this.domainsService
      .getDomain(this.domainId)
      .subscribe(result => {
        this.domain = result;
      });
  }

  // endregion

  // region actions

  createRecord(): void {
    const ref = this.dialogService
      .open(EditRecordComponent, {
        data: {
          domainId: this.domain?.id,
          domainName: this.domain?.domainName,
        }
      });
    ref.afterClosed
      .subscribe(result => {
        if (result) {
          this.loadDomain();
        }
      }, error => this.notifier.showErrorToast(error));
  }

  edit(): void {
    const ref = this.dialogService
      .open(EditDomainComponent, {
        data: { domain: this.domain }
      });
    ref.afterClosed
      .subscribe(result => {
        if (result) {
          this.loadDomain();
        }
      }, error => this.notifier.showErrorToast(error));
  }

  promptDelete(): void {
    const ref = this.dialogService
      .confirm('domains.delete.message',
        this.domain?.domainName,
        'danger');

    ref.afterClosed
      .subscribe(result => {
        if (result === true) {
          this.deleteDomain();
        }
      });
  }

  private deleteDomain(): void {
    if (this.domain?.id == null) {
      return;
    }

    this.domainsService
      .deleteDomain(this.domain.id)
      .subscribe(result => {
        this.notifier
          .showSuccessToast('domains.delete.deleted', true);
        this.navigationService
          .goToUrl(this.navigationService.getDashboardLink());
      }, error => this.notifier
        .showErrorToast(error));
  }

  // endregion

  // region getters

  getHostname(record: Record): string {
    return utilities.isNullOrWhitespace(record.prefix)
      ? (this.domain?.domainName ?? '')
      : `${record.prefix}.${this.domain?.domainName}`;
  }

  getRecordDetailsLink(record: Record): string {
    if (this.domainId == null || record?.id == null) {
      return '';
    }
    return this.navigationService.getRecordDetailsLink(this.domainId, record.id);
  }

  // endregion
}
