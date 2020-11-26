import { Component } from '@angular/core';
import {TranslateService} from '@ngx-translate/core';
import {Languages} from './shared/models/app/languages';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  constructor(
    private readonly translator: TranslateService
  ) {
    translator.setDefaultLang(Languages.EN);
  }
}
