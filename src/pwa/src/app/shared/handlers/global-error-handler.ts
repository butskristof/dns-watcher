import {ErrorHandler, Injectable} from '@angular/core';
import {NavigationService} from '../services/navigation.service';
import {AuthService} from '../../auth/services/auth.service';

@Injectable()
export class GlobalErrorHandler implements ErrorHandler {
  constructor(
    private authService: AuthService,
    private navigationService: NavigationService
  ) {
  }

  handleError(error: any): void {
    // if (error && error.message && error.message.toLowerCase().indexOf('loading chunk') > -1) {
    //   window.location.reload(true);
    // }

    if (error === 'failedToRefreshToken') { // TODO
      this.authService.logout();
      this.navigationService.goToLogin();
    } else {
      console.log(error);
    }
  }
}
