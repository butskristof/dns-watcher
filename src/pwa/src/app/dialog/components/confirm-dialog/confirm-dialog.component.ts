import { Component, OnInit } from '@angular/core';
import {DialogConfig} from '../../models/dialog-config';
import {DialogRef} from '../../models/dialog-ref';

@Component({
  selector: 'app-confirm-dialog',
  templateUrl: './confirm-dialog.component.html',
  styleUrls: ['./confirm-dialog.component.scss']
})
export class ConfirmDialogComponent implements OnInit {

  constructor(
    private readonly config: DialogConfig,
    private readonly dialog: DialogRef
  ) { }

  ngOnInit(): void {
  }

  confirm(): void {
    this.dialog.close(true);
  }

  cancel(): void {
    this.dialog.close();
  }

  get message(): string {
    return this.config?.data?.message ?? '';
  }

  get content(): string | null {
    return this.config?.data?.content ?? null;
  }
}
