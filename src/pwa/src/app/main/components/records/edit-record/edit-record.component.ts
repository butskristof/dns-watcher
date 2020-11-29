import { Component, OnInit } from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {Record} from '../../../models/entities/domains/record';
import {CreateOrUpdateRecordData} from '../../../models/data/domains/create-or-update-record-data';
import {DialogConfig} from '../../../../dialog/models/dialog-config';
import {DialogRef} from '../../../../dialog/models/dialog-ref';
import {ErrorService} from '../../../../shared/services/error.service';
import {NotifierService} from '../../../../shared/services/notifier.service';
import {RecordsService} from '../../../services/records.service';
import {RecordType} from '../../../models/entities/domains/record-type';
import {Config} from '../../../../config';

@Component({
  selector: 'app-edit-record',
  templateUrl: './edit-record.component.html',
  styleUrls: ['./edit-record.component.scss']
})
export class EditRecordComponent implements OnInit {

  // region properties
  form?: FormGroup;
  error?: string;

  recordTypes = Config.recordType;

  private record?: Record;
  private domainId?: string;
  private domainName?: string;
  private data = new CreateOrUpdateRecordData();

  // endregion

  constructor(
    private readonly config: DialogConfig,
    private readonly dialog: DialogRef,
    private readonly formBuilder: FormBuilder,
    private readonly errorService: ErrorService,
    private readonly notifier: NotifierService,
    private readonly recordsService: RecordsService
  ) { }

  ngOnInit(): void {
    if (this.config.data.domainId) {
      this.domainId = this.config.data.domainId;
    }
    if (this.config.data.domainName) {
      this.domainName = this.config.data.domainName;
    }
    if (this.config.data.record) {
      this.record = this.config.data.record;
      this.data = new CreateOrUpdateRecordData(this.record);
    }

    this.buildForm();
  }

  // region form
  private buildForm(): void {
    this.form = this.formBuilder.group({
      recordType: [this.data.recordType],
      prefix: [this.data.prefix],
      expectedValue: [this.data.expectedValue, Validators.required],
      expectedTimeToLive: [this.data.expectedTimeToLive, Validators.required]
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
    console.log('hello');
    if (!this.error && this.domainId) {
      this.recordsService
        .saveRecord(this.domainId, this.data)
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
    return 'records.edit.title-' + (this.forCreate
      ? 'create' : 'edit');
  }

  // endregion
}
