import { Component, OnInit } from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {ActivatedRoute} from '@angular/router';
import {AuthService} from '../../services/auth.service';
import {NavigationService} from '../../../shared/services/navigation.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  private returnUrl: string = this.navigationService.getDashboardLink();

  form?: FormGroup;
  error?: string;
  loading = false;

  constructor(
    private readonly route: ActivatedRoute,
    private readonly formBuilder: FormBuilder,
    private readonly authService: AuthService,
    private readonly navigationService: NavigationService
  ) {}

  ngOnInit(): void {
    this.setupQueryParamListener();
    this.buildForm();
  }

  // region return url

  private setupQueryParamListener(): void {
    this.route
      .queryParamMap
      .subscribe(params => {
        const url = params.get('returnUrl');
        if (url) {
          this.returnUrl = url;
        }
      });
  }

  // endregion

  // region form

  private buildForm(): void {
    this.form = this.formBuilder.group({
      username: [null, Validators.required],
      password: [null, Validators.required],
      remember: [false],
    });
  }

  private clearError(): void {
    this.error = undefined;
  }

  // endregion

  // region actions

  submit(): void {
    this.clearError();

    const username = this.form?.get('username')?.value;
    const password = this.form?.get('password')?.value;
    const remember = this.form?.get('remember')?.value;

    if (!this.error) {
      this.authService
        .login(username, password, remember)
        .subscribe(result => {
          if (result) {
            this.navigationService.goToUrl(this.returnUrl);
          }
        }, error => this.error = error.message);
    }
  }

  // endregion
}
