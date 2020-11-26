import {Injectable} from '@angular/core';
import {Router} from '@angular/router';
import {Location} from '@angular/common';

@Injectable({
  providedIn: 'root'
})
export class NavigationService {
  constructor(
    private readonly router: Router,
    private readonly location: Location
  ) {
  }

  // region actions

  // region general

  goBack(): void {
    this.location.back();
  }

  // endregion

  // region login

  goToLogin(returnUrl: string | null = null): Promise<boolean> {
    return this.router.navigate(['/auth/login'], {
      queryParams: {
        returnUrl
      }
    });
  }

  // endregion

  // endregion

  goToUrl(url: string | null = null): Promise<boolean> {
    return this.router.navigate([`/${url}`]);
  }

  getDomainDetailsLink(id: string): string {
    return `/domains/${id}`;
  }

  getDashboardLink(): string {
    return `/dashboard`;
  }
}
