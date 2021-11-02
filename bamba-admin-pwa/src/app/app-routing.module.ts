import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { OktaCallbackComponent, OktaAuthGuard } from '@okta/okta-angular';

import { Routes, RouterModule } from '@angular/router';

import { HomeComponent } from './components/home/home.component';
import { SearchComponent } from './components/search/search.component';

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'search', component: SearchComponent, canActivate: [OktaAuthGuard] },
  { path: 'callback', component: OktaCallbackComponent }
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
