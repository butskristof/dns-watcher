import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HeaderComponent } from './components/header/header.component';
import {RouterModule} from '@angular/router';
import {TranslateModule} from '@ngx-translate/core';
import { ListItemButtonComponent } from './components/lists/list-item-button/list-item-button.component';

@NgModule({
  declarations: [HeaderComponent, ListItemButtonComponent],
  exports: [
    HeaderComponent,
    ListItemButtonComponent
  ],
  imports: [
    CommonModule,
    RouterModule,
    TranslateModule
  ]
})
export class SharedModule { }
