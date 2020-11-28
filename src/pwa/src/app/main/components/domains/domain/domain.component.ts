import {Component, Input, OnChanges, OnInit, SimpleChanges} from '@angular/core';
import {Domain} from '../../../models/entities/domains/domain';
import {DomainsService} from '../../../services/domains.service';
import {Record} from '../../../models/entities/domains/record';
import utilities from '../../../../shared/helpers/utilities';
import {RecordType} from '../../../models/entities/domains/record-type';
import {NavigationService} from '../../../../shared/services/navigation.service';
import {ActionButtonStyle} from '../../../../shared/models/viewmodels/action-button-style';

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

  activeRecord: Record | null = null;

  constructor(
    private readonly domainsService: DomainsService,
    private readonly navigationService: NavigationService
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
      .subscribe(result => this.domain = result);
  }

  // endregion

  // region getters

  get stringified(): string {
    return this.domain == null
      ? ''
      : JSON.stringify(this.domain, null, 2);
  }

  getHostname(record: Record): string {
    return utilities.isNullOrWhitespace(record.prefix)
      ? (this.domain?.domainName ?? '')
      : `${record.prefix}.${this.domain?.domainName}`;
  }

  // endregion

  // region actions

  setActiveRecord(record: Record): void {
    this.activeRecord = record;
  }

  edit(): void {

  }

  promptDelete(): void {

  }

  // endregion

  getRecordDetailsLink(record: Record): string {
    if (this.domainId == null || record?.id == null) {
      return '';
    }
    return this.navigationService.getRecordDetailsLink(this.domainId, record.id);
  }
}
