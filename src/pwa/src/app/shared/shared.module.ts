import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HeaderComponent } from './components/header/header.component';
import {RouterModule} from '@angular/router';
import {TranslateModule} from '@ngx-translate/core';
import { ActionButtonComponent } from './components/utilities/action-button/action-button.component';

@NgModule({
  declarations: [
    HeaderComponent,
    ActionButtonComponent,
  ],
  exports: [
    HeaderComponent,
    ActionButtonComponent,
  ],
  imports: [
    CommonModule,
    RouterModule,
    TranslateModule
  ]
})
export class SharedModule { }
