import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

const routes: Routes = [
  {
    path: 'auth',
    loadChildren: () => import('./auth/auth.module').then(m => m.AuthModule)
  },
  {
    path: '',
    loadChildren: () => import('./main/main.module').then(m => m.MainModule)
  },
  {
    path: '**',
    redirectTo: '/dashboard'
  }
];
//   {
//     path: 'browse',
//     loadChildren: () => import('./browse/browse.module').then(m => m.BrowseModule)
//   },
//   {
//     path: '**',
//     redirectTo: '/browse'
//   }
// ]

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
