import { Component, OnInit } from '@angular/core';
import {DialogConfig} from '../../../../dialog/models/dialog-config';
import {DialogRef} from '../../../../dialog/models/dialog-ref';
import {DnsServer} from '../../../models/entities/servers/dns-server';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {ErrorService} from '../../../../shared/services/error.service';
import {DnsServersService} from '../../../services/dns-servers.service';
import {NotifierService} from '../../../../shared/services/notifier.service';
import {CreateOrUpdateDnsServerData} from '../../../models/data/create-or-update-dns-server-data';

@Component({
  selector: 'app-edit-server',
  templateUrl: './edit-server.component.html',
  styleUrls: ['./edit-server.component.scss']
})
export class EditServerComponent implements OnInit {
  // region properties
  form?: FormGroup;
  error?: string;

  private server?: DnsServer;
  private data = new CreateOrUpdateDnsServerData();

  // endregion

  constructor(
    private readonly config: DialogConfig,
    private readonly dialog: DialogRef,
    private readonly formBuilder: FormBuilder,
    private readonly errorService: ErrorService,
    private readonly serversService: DnsServersService,
    private readonly notifier: NotifierService
  ) { }

  ngOnInit(): void {
    if (this.config.data.server) {
      this.server = this.config.data.server;
      this.data = new CreateOrUpdateDnsServerData(this.server);
    }

    this.buildForm();
  }

  // region form
  private buildForm(): void {
    this.form = this.formBuilder.group({
      name: [this.data.name, Validators.required],
      ipAddress: [this.data.ipAddress, Validators.required],
      port: [this.data.port, Validators.required],
    });
  }

  private clearError(): void {
    this.error = undefined;
  }
  // endregion

  // region actions
  save(): void {
    this.clearError();

    Object.assign(this.data, this.form?.value);

    if (!this.error) {
      console.log(this.data);
      // save
      this.dialog.close();
    }
  }

  cancel(): void {
    this.dialog.close();
  }
  // endregion

  // region getters
  get forCreate(): boolean {
    return !this.data.id;
  }
  get title(): string {
    return 'dns-servers.edit.title-' + (this.forCreate
      ? 'create' : 'edit');
  }
  // endregion
}
