import { Component, OnInit } from '@angular/core';
import {DnsServer} from '../../../models/entities/servers/dns-server';
import {DnsServersService} from '../../../services/dns-servers.service';
import {ActionButtonStyle} from '../../../../shared/models/viewmodels/action-button-style';
import {DialogService} from '../../../../dialog/services/dialog.service';
import {ExampleComponent} from '../../../../dialog/components/example/example.component';
import {ConfirmDialogComponent} from '../../../../dialog/components/confirm-dialog/confirm-dialog.component';
import {filter} from 'rxjs/operators';
import {NotifierService} from '../../../../shared/services/notifier.service';
import {EditServerComponent} from '../edit-server/edit-server.component';

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
    this.edit();
  }

  // region fetch data

  private loadServers(): void {
    this.serversService
      .getDnsServers()
      .subscribe(result => this.servers = result.dnsServers,
        error => this.notifier.showErrorToast(error));
  }

  // endregion

  // region actions

  edit(server?: DnsServer): void {
    const ref = this.dialogService
      .open(EditServerComponent, {
        data: {server}
      });
    ref.afterClosed
      .subscribe(result => {
        if (result) {
          this.loadServers();
        }
      }, error => this.notifier.showErrorToast(error));
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
      .subscribe(result => {
        if (result) {
          this.notifier
            .showSuccessToast('dns-servers.delete.deleted', true);
          this.loadServers();
        }
      }, error => this.notifier
        .showErrorToast(error));
  }
  // endregion
}
