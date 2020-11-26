import { BrowserModule } from '@angular/platform-browser';
import {APP_INITIALIZER, NgModule} from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import {registerLocaleData} from '@angular/common';
import localeEnBe from '@angular/common/locales/en-BE';
import {HttpClient, HttpClientModule} from '@angular/common/http';
import {Config} from './config';
import {TranslateHttpLoader} from '@ngx-translate/http-loader';
import {TranslateLoader, TranslateModule} from '@ngx-translate/core';

registerLocaleData(localeEnBe);

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    HttpClientModule,
    BrowserModule,
    AppRoutingModule,
    TranslateModule.forRoot({
      loader: {
        provide: TranslateLoader,
        useFactory: HttpLoaderFactory,
        deps: [HttpClient]
      }
    }),
  ],
  providers: [
    {
      provide: APP_INITIALIZER,
      useFactory: initApp,
      multi: true,
      deps: [HttpClient]
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

export function initApp(http: HttpClient): () => void {
  return async () => {
    await http
      .get<any>('/assets/app-settings.json?' + new Date().getTime())
      .toPromise()
      .then(async response => {
        Config.apiUrl = response.apiUrl;

        await http
          .get<any>(Config.apiUrl + '/config')
          .toPromise()
          .then(result => {
            Config.version = result.applicationInfo.version;
            Config.environment = result.applicationInfo.environment;
          });
      });
  };
}

export function HttpLoaderFactory(http: HttpClient): TranslateHttpLoader {
  return new TranslateHttpLoader(
    http,
    '../../assets/i18n/',
    '.json?' + new Date().getTime()
  );
}
