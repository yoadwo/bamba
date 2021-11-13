import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { OktaCallbackComponent, OktaAuthGuard } from '@okta/okta-angular';
import { Routes, RouterModule } from '@angular/router';
import { NotFoundComponent } from './components/not-found/not-found.component';

import { HomeComponent } from './components/home/home.component';

const routes: Routes = [
  { path: '', component: HomeComponent, canActivate: [OktaAuthGuard]},
  { path: 'callback', component: OktaCallbackComponent },
  { path: '404', component: NotFoundComponent},
  { path: '**', redirectTo: '/404'} 
  
];

@NgModule({
  declarations: [],
  imports: [
    //CommonModule
    RouterModule.forRoot(routes)
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
