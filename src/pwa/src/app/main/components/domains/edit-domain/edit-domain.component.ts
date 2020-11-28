import {Component, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {Domain} from '../../../models/entities/domains/domain';
import {DialogConfig} from '../../../../dialog/models/dialog-config';
import {DialogRef} from '../../../../dialog/models/dialog-ref';
import {ErrorService} from '../../../../shared/services/error.service';
import {NotifierService} from '../../../../shared/services/notifier.service';
import {CreateOrUpdateDomainData} from '../../../models/data/domains/create-or-update-domain-data';
import {DomainsService} from '../../../services/domains.service';

@Component({
  selector: 'app-edit-domain',
  templateUrl: './edit-domain.component.html',
  styleUrls: ['./edit-domain.component.scss']
})
export class EditDomainComponent implements OnInit {
  // region properties
  form?: FormGroup;
  error?: string;

  private domain?: Domain;
  private data = new CreateOrUpdateDomainData();
  // endregion

  constructor(
    private readonly config: DialogConfig,
    private readonly dialog: DialogRef,
    private readonly formBuilder: FormBuilder,
    private readonly errorService: ErrorService,
    private readonly notifier: NotifierService,
    private readonly domainsService: DomainsService
  ) {
  }

  ngOnInit(): void {
    if (this.config.data.domain) {
      this.domain = this.config.data.domain;
      this.data = new CreateOrUpdateDomainData(this.domain);
    }

    this.buildForm();
  }

  // region form
  private buildForm(): void {
    this.form = this.formBuilder.group({
      domainName: [this.data.domainName, Validators.required]
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
      this.domainsService
        .saveDomain(this.data)
        .subscribe(result => {
          this.dialog.close(result);
          this.notifier.showSuccessToast('common.saved', true);
        }, error => this.error = this.errorService
          .getErrorMessage(error));
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
    return 'domains.edit.title-' + (this.forCreate
      ? 'create' : 'edit');
  }

  // endregion
}
