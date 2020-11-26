import {Component, OnDestroy, OnInit} from '@angular/core';
import {NavigationService} from '../../../../shared/services/navigation.service';
import {Subscription} from 'rxjs';
import {ActivatedRoute} from '@angular/router';

@Component({
  selector: 'app-domain-page',
  templateUrl: './domain-page.component.html',
  styleUrls: ['./domain-page.component.scss']
})
export class DomainPageComponent
  implements OnInit, OnDestroy
{
  private subDomainId?: Subscription;
  domainId: string | null = null;

  constructor(
    private readonly navigationService: NavigationService,
    private readonly route: ActivatedRoute,
  ) { }

  ngOnInit(): void {
    this.setupListeners();
  }

  ngOnDestroy(): void {
    this.releaseSubscriptions();
  }

  // region listeners
  private setupListeners(): void {
    this.subDomainId = this.route
      .paramMap
      .subscribe(params => {
        this.domainId = params.get('id');
      });
  }

  private releaseSubscriptions(): void {
    this.subDomainId?.unsubscribe();
  }
  // endregion

  // region getters
  get backLink(): string {
    return this.navigationService.getDashboardLink();
  }
  // endregion
}
