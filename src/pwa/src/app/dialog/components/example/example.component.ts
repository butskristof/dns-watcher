import { Component } from '@angular/core';
import {DialogConfig} from '../../models/dialog-config';
import {DialogRef} from '../../models/dialog-ref';

@Component({
  selector: 'app-example',
  templateUrl: './example.component.html',
  styleUrls: ['./example.component.scss'],
})
export class ExampleComponent {
  constructor(
    public config: DialogConfig,
    public dialog: DialogRef
  ) {
  }

  onClose(): void {
    this.dialog.close('some value');
  }
}
