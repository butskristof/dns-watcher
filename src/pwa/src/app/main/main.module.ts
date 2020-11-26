import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {MainRoutingModule} from './main-routing.module';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { DashboardPageComponent } from './pages/dashboard-page/dashboard-page.component';

@NgModule({
  declarations: [DashboardComponent, DashboardPageComponent],
  imports: [
    CommonModule,
    MainRoutingModule
  ]
})
export class MainModule { }
