import { Component, OnInit } from '@angular/core';
import {AuthService} from '../../../auth/services/auth.service';
import {NavigationService} from '../../services/navigation.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {

  constructor(
    private readonly authService: AuthService,
    private readonly navigationService: NavigationService
  ) { }

  ngOnInit(): void {
  }

  // region getters
  get isAuthenticated(): boolean {
    return this.authService.isAuthenticated();
  }
  // endregion

  // region actions
  logout(): void {
    this.authService.logout();
    this.navigationService.goToLogin();
  }
  // endregion
}
