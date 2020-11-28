import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DialogComponent } from './components/dialog/dialog.component';
import { InsertionDirective } from './directives/insertion.directive';
import { ExampleComponent } from './components/example/example.component';

// https://malcoded.com/posts/angular-dynamic-components/

@NgModule({
  declarations: [DialogComponent, InsertionDirective, ExampleComponent],
  imports: [
    CommonModule
  ]
})
export class DialogModule { }
