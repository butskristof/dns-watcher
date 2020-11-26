import {Component, OnDestroy, OnInit} from '@angular/core';
import {Subscription} from 'rxjs';
import {NavigationService} from '../../../../shared/services/navigation.service';
import {ActivatedRoute} from '@angular/router';

@Component({
  selector: 'app-record-page',
  templateUrl: './record-page.component.html',
  styleUrls: ['./record-page.component.scss']
})
export class RecordPageComponent
  implements OnInit, OnDestroy
{
  private subParams?: Subscription;
  domainId: string | null = null;
  recordId: string | null = null;

  constructor(
    private readonly navigationService: NavigationService,
    private readonly route: ActivatedRoute
  ) { }

  ngOnInit(): void {
    this.setupListeners();
  }

  ngOnDestroy(): void {
    this.releaseSubscriptions();
  }

  // region listeners
  private setupListeners(): void {
    this.subParams = this.route
      .paramMap
      .subscribe(params => {
        this.domainId = params.get('domainId');
        this.recordId = params.get('recordId');
      });
  }

  private releaseSubscriptions(): void {
    this.subParams?.unsubscribe();
  }
  // endregion

  // region getters
  get backLink(): string {
    if (this.domainId == null) {
      return '';
    }
    return this.navigationService.getDomainDetailsLink(this.domainId);
  }
  // endregion
}
