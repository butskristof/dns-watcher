import { Component, OnInit } from '@angular/core';
import {Domain} from '../../../models/entities/domains/domain';
import {DomainsService} from '../../../services/domains.service';
import {NavigationService} from '../../../../shared/services/navigation.service';
import {DialogService} from '../../../../dialog/services/dialog.service';
import {EditDomainComponent} from '../edit-domain/edit-domain.component';
import {NotifierService} from '../../../../shared/services/notifier.service';

@Component({
  selector: 'app-domains-list',
  templateUrl: './domains-list.component.html',
  styleUrls: ['./domains-list.component.scss']
})
export class DomainsListComponent implements OnInit {
  domains: Domain[] = [];

  constructor(
    private readonly domainsService: DomainsService,
    private readonly navigationService: NavigationService,
    private readonly dialogService: DialogService,
    private readonly notifier: NotifierService
  ) { }

  ngOnInit(): void {
    this.loadDomains();
  }

  // region fetch data
  private loadDomains(): void {
    this.domainsService
      .getDomains()
      .subscribe(result => this.domains = result.domains);
  }
  // endregion

  // region actions
  create(): void {
    const ref = this.dialogService
      .open(EditDomainComponent, {
        data: {}
      });
    ref.afterClosed
      .subscribe(result => {
        if (result) {
          this.loadDomains();
        }
      }, error => this.notifier.showErrorToast(error));
  }
  // endregion

  // region getters
  getDomainDetailsLink(domain: Domain): string {
    if (domain.id == null) { return ''; }
    return this.navigationService.getDomainDetailsLink(domain.id);
  }
  // endregion
}
