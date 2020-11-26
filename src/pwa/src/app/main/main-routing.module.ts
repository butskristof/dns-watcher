import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {DashboardPageComponent} from './pages/dashboard-page/dashboard-page.component';
import {AuthGuard} from '../auth/guards/auth.guard';
import {DomainPageComponent} from './pages/domains/domain-page/domain-page.component';

const routes: Routes = [
  {
    path: 'dashboard',
    component: DashboardPageComponent,
    canActivate: [AuthGuard]
  },
  {
    path: 'domains/:id',
    component: DomainPageComponent,
    canActivate: [AuthGuard]
  },
  {
    path: '**',
    redirectTo: '/dashboard'
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MainRoutingModule {}
