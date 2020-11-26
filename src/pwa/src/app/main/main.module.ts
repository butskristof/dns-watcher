import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {MainRoutingModule} from './main-routing.module';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { DashboardPageComponent } from './pages/dashboard-page/dashboard-page.component';
import { DomainsListComponent } from './components/domains/domains-list/domains-list.component';
import { DomainPageComponent } from './pages/domains/domain-page/domain-page.component';
import { DomainComponent } from './components/domains/domain/domain.component';
import {TranslateModule} from '@ngx-translate/core';
import { RecordPageComponent } from './pages/records/record-page/record-page.component';
import { RecordComponent } from './components/records/record/record.component';
import { DnsServersListComponent } from './components/dns-servers/dns-servers-list/dns-servers-list.component';

@NgModule({
  declarations: [DashboardComponent, DashboardPageComponent, DomainsListComponent, DomainPageComponent, DomainComponent, RecordPageComponent, RecordComponent, DnsServersListComponent],
  imports: [
    CommonModule,
    MainRoutingModule,
    TranslateModule
  ]
})
export class MainModule { }
