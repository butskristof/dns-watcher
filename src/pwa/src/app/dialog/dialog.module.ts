import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DialogComponent } from './components/dialog/dialog.component';
import { InsertionDirective } from './directives/insertion.directive';
import { ExampleComponent } from './components/example/example.component';
import { ConfirmDialogComponent } from './components/confirm-dialog/confirm-dialog.component';
import {TranslateModule} from '@ngx-translate/core';

// https://malcoded.com/posts/angular-dynamic-components/

@NgModule({
  declarations: [
    DialogComponent,
    InsertionDirective,
    ExampleComponent,
    ConfirmDialogComponent
  ],
  imports: [
    CommonModule,
    TranslateModule
  ]
})
export class DialogModule { }
