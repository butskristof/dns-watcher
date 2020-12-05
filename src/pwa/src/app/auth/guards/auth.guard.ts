import {Injectable} from '@angular/core';
import {CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree} from '@angular/router';
import {NavigationService} from '../../shared/services/navigation.service';
import {AuthService} from '../services/auth.service';
import {Observable, of} from 'rxjs';
import {catchError, map} from 'rxjs/operators';

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
    : boolean | Promise<boolean> | Observable<boolean> {
    if (this.authService.isAuthenticated()) {
      return true;
    }

    return this.authService
      .tryRefreshToken()
      .pipe(
        map(e => true),
        catchError((err, caught) => {
            console.error('Not authenticated, redirecting to login.');

            this.navigationService.goToLogin(state.url);
            return of(false);
        })
      );
      // .subscribe(() => {
      //   return of(true);
      // }, error => {
      // });
  }

}
