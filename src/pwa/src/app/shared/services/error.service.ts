import {Injectable} from '@angular/core';
import {TranslateService} from '@ngx-translate/core';
import {NavigationService} from './navigation.service';
import {AuthService} from '../../auth/services/auth.service';
import {HttpErrorResponse} from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ErrorService {
  // region construction
  constructor(
    private translator: TranslateService,
    private navigationService: NavigationService,
    private authService: AuthService
  ) {
  }
  // endregion

  public getErrorMessage(
    error: Error | HttpErrorResponse | string)
    : string {
    // console.log(error);

    let message = '';

    if (error instanceof HttpErrorResponse) {
      message = this.getServerErrorMessage(error);
    } else if (error instanceof Error) {
      message = this.getClientErrorMessage(error);
    } else if (error === 'failedToRefreshToken') {
      this.authService.logout();
      this.navigationService.goToLogin();
    } else {
      message = this.translator.instant(error);
    }

    return message;
  }

  private getClientErrorMessage(error: Error): string {
    return this.translator.instant('errors.unexpected');
  }

  private getServerErrorMessage(error: HttpErrorResponse): string {
    return navigator.onLine
      ? this.composeMessage(error)
      : this.translator.instant('errors.no-connection');
  }

  private composeMessage(error: HttpErrorResponse): string {
    if (error.status === 400) {
      let msg = '';

      if (error?.error?.errors) {
        const errorList = error.error.errors;
        for (const fieldName in errorList) {
          if (errorList.hasOwnProperty(fieldName)) {
            for (const errorKey in errorList[fieldName]) {
              if (true) {
                if (fieldName) {
                  const modifiedFieldName = fieldName[0].toLocaleLowerCase() + fieldName.substring(1);
                  msg += this.translator.instant('fields.' + modifiedFieldName) + ' ';
                }
                if (errorList[fieldName][errorKey]) {

                  const modifiedErrorName = errorList[fieldName][errorKey][0].toLocaleLowerCase()
                    + errorList[fieldName][errorKey].substring(1);

                  msg += this.translator.instant(
                    'errors.' + modifiedErrorName
                  );
                }

                msg += '<br />';
                // }
              }
            }
          }
        }
      }

      /*
      if (error?.error?.validationErrors) {
        const errorList = error.error.validationErrors;

        errorList.forEach(validationError => {
          if (validationError.validationErrorType !== 5) {
            msg += this.translator.instant(
              'fields.' + validationError.fieldInfo
            ) + ' ' + this.translator.instant(
              `identityServer.validationErrors.${validationError.validationErrorType}`
            );
          } else {
            msg += this.translator.instant(`identityServer.validationErrors.${validationError.messageKey}`);
          }

          msg += '<br />';
        });
      }
       */

      return msg;
    }

    if (error.status === 403) {
      return this.translator.instant('errors.forbidden');
    }

    if (error.status === 404) {
      return this.translator.instant('errors.not-found');
    }

    if (error.status === 409) {
      return this.translator.instant('errors.conflict');
    }

    return this.translator.instant('errors.unexpected');
  }
}
