import { Component, OnInit } from '@angular/core';
import {DnsServer} from '../../../models/entities/servers/dns-server';
import {DnsServersService} from '../../../services/dns-servers.service';
import {ActionButtonStyle} from '../../../../shared/models/viewmodels/action-button-style';
import {DialogService} from '../../../../dialog/services/dialog.service';
import {ExampleComponent} from '../../../../dialog/components/example/example.component';
import {log} from 'util';

@Component({
  selector: 'app-dns-servers-list',
  templateUrl: './dns-servers-list.component.html',
  styleUrls: ['./dns-servers-list.component.scss']
})
export class DnsServersListComponent implements OnInit {
  servers: DnsServer[] = [];

  actionButtonStyles = ActionButtonStyle;

  constructor(
    private readonly serversService: DnsServersService,
    private readonly dialogService: DialogService
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

  // region actions

  edit(server: DnsServer | null = null): void {
    console.log(`edit ${server?.name}`);
    const ref = this.dialogService.open(ExampleComponent, {
      data: {
        message: 'I am dynamic.'
      }
    });

    ref.afterClosed
      .subscribe(result => console.log(result));
  }

  promptDeleteServer(server: DnsServer): void {
    console.log('delete');
  }

  private deleteServer(id: string): void {

  }
  // endregion
}
