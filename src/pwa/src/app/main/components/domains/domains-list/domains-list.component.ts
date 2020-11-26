import { Component, OnInit } from '@angular/core';
import {Domain} from '../../../models/entities/domain';
import {DomainsService} from '../../../services/domains.service';
import {NavigationService} from '../../../../shared/services/navigation.service';

@Component({
  selector: 'app-domains-list',
  templateUrl: './domains-list.component.html',
  styleUrls: ['./domains-list.component.scss']
})
export class DomainsListComponent implements OnInit {
  domains: Domain[] = [];

  constructor(
    private readonly domainsService: DomainsService,
    private readonly navigationService: NavigationService
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

  // region getters
  getDomainDetailsLink(domain: Domain): string {
    if (domain.id == null) { return ''; }
    return this.navigationService.getDomainDetailsLink(domain.id);
  }
  // endregion
}
