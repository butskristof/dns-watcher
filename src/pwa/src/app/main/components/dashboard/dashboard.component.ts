import { Component, OnInit } from '@angular/core';
import {Domain} from '../../models/entities/domain';
import {DomainsService} from '../../services/domains.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {
  domains: Domain[] = [];

  constructor(
    private readonly domainsService: DomainsService
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
}
