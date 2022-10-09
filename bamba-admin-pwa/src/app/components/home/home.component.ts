import { Component, OnInit } from '@angular/core';
import { OktaAuthService } from '@okta/okta-angular';
import { BaseUrlService } from 'src/app/services/baseUrl/base-url.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  url: string = '';
  isAuthenticated = false;

  constructor(
    private baseUrl: BaseUrlService,
    public oktaAuth: OktaAuthService) {
      // this.oktaAuth.$authenticationState.subscribe(
      //   (isAuthenticated: boolean) => this.isAuthenticated = isAuthenticated
      // );
     }

  async ngOnInit(): Promise<void> {
    this.url = this.baseUrl.url;

    // this.isAuthenticated = await this.oktaAuth.isAuthenticated();
    this.isAuthenticated = true;
  }

  async logout(): Promise<void> {
    await this.oktaAuth.signOut();
  }

}
