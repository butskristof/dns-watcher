import { Component, OnInit } from '@angular/core';
import {DnsServer} from '../../../models/entities/servers/dns-server';
import {DnsServersService} from '../../../services/dns-servers.service';
import {ActionButtonStyle} from '../../../../shared/models/viewmodels/action-button-style';
import {DialogService} from '../../../../dialog/services/dialog.service';
import {ExampleComponent} from '../../../../dialog/components/example/example.component';
import {ConfirmDialogComponent} from '../../../../dialog/components/confirm-dialog/confirm-dialog.component';
import {filter} from 'rxjs/operators';
import {NotifierService} from '../../../../shared/services/notifier.service';

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
    private readonly dialogService: DialogService,
    private readonly notifier: NotifierService
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

  edit(server?: DnsServer): void {
  }

  promptDeleteServer(server: DnsServer): void {
    const ref = this.dialogService
      .confirm('dns-servers.delete.message',
        server.pretty,
        'danger');

    ref.afterClosed
      .subscribe(result => {
        if (result === true) {
          this.deleteServer(server.id);
        }
      });
  }

  private deleteServer(id?: string): void {
    if (id == null) {
      return;
    }

    this.serversService
      .deleteDnsServer(id)
      .subscribe(() => {
        this.notifier.showSuccessToast('dns-servers.delete.deleted', true);
        this.loadServers();
      });
  }
  // endregion
}
