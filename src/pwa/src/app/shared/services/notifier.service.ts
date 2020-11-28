import { Injectable } from '@angular/core';
import {ToastrService} from 'ngx-toastr';
import {TranslateService} from '@ngx-translate/core';
import {HttpErrorResponse} from '@angular/common/http';
import {ErrorService} from './error.service';

@Injectable({
  providedIn: 'root'
})
export class NotifierService {

  constructor(
    private errorService: ErrorService,
    private translator: TranslateService,
    private toastr: ToastrService
  ) { }

  public showSuccessToast(
    message: string,
    translate: boolean = false)
    : void
  {
    if (translate) {
      message = this.translator.instant(message);
    }

    this.toastr.success(message, '', {
      enableHtml: true,
    });
  }

  public showErrorToast(
    error: Error | HttpErrorResponse | string)
    : void {
    const message = this.errorService.getErrorMessage(error);
    if (message) {
      this.toastr.error(message, '', {
        enableHtml: true,
      });
    }
  }
}
