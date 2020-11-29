import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree } from '@angular/router';
import {NavigationService} from '../../shared/services/navigation.service';
import {AuthService} from '../services/auth.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(
    private readonly navigationService: NavigationService,
    private readonly authService: AuthService
  ) {
  }

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot)
    : boolean {
    if (this.authService.isAuthenticated()) {
      return true;
    }

    console.error('Not authenticated, redirecting to login.');

    this.navigationService.goToLogin(state.url);
    return false;
  }

}
