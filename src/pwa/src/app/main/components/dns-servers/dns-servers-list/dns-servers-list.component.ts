import { Component, OnInit } from '@angular/core';
import {DnsServer} from '../../../models/entities/servers/dns-server';
import {DnsServersService} from '../../../services/dns-servers.service';
import {ActionButtonStyle} from '../../../../shared/models/viewmodels/action-button-style';

@Component({
  selector: 'app-dns-servers-list',
  templateUrl: './dns-servers-list.component.html',
  styleUrls: ['./dns-servers-list.component.scss']
})
export class DnsServersListComponent implements OnInit {
  servers: DnsServer[] = [];

  actionButtonStyles = ActionButtonStyle;

  constructor(
    private readonly serversService: DnsServersService
  ) { }

  ngOnInit(): void {
    this.loadServers();
  }

  // region fetch data
  private loadServers(): void {
    this.serversService
      .getDnsServers()
      .subscribe(result => this.servers = result.dnsServers);
  }
  // endregion

}
