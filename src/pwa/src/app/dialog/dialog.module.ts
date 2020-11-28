import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DialogComponent } from './components/dialog/dialog.component';
import { InsertionDirective } from './directives/insertion.directive';
import { ConfirmDialogComponent } from './components/confirm-dialog/confirm-dialog.component';
import {TranslateModule} from '@ngx-translate/core';

// https://malcoded.com/posts/angular-dynamic-components/

@NgModule({
  declarations: [
    DialogComponent,
    InsertionDirective,
    ConfirmDialogComponent
  ],
  imports: [
    CommonModule,
    TranslateModule
  ]
})
export class DialogModule { }
