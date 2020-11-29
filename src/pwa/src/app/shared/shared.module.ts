import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HeaderComponent } from './components/header/header.component';
import {RouterModule} from '@angular/router';
import {TranslateModule} from '@ngx-translate/core';
import { ActionButtonComponent } from './components/utilities/action-button/action-button.component';
import { EnvironmentIndicatorComponent } from './components/utilities/environment-indicator/environment-indicator.component';

@NgModule({
  declarations: [
    HeaderComponent,
    ActionButtonComponent,
    EnvironmentIndicatorComponent,
  ],
  exports: [
    HeaderComponent,
    ActionButtonComponent,
    EnvironmentIndicatorComponent,
  ],
  imports: [
    CommonModule,
    RouterModule,
    TranslateModule
  ]
})
export class SharedModule { }
