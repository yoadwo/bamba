import { Component, OnInit } from '@angular/core';
import { NgrokUrlService } from 'src/app/services/ngrokUrl/ngrok-url.service';
import { OktaAuthService } from '@okta/okta-angular';
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
    private ngrokUrl: NgrokUrlService,
    public oktaAuth: OktaAuthService) {
      this.oktaAuth.$authenticationState.subscribe(
        (isAuthenticated: boolean) => this.isAuthenticated = isAuthenticated
      );
     }

  async ngOnInit(): Promise<void> {
    if (!environment.production){
      this.url += environment.baseUrl;
    } else {
      this.url += this.ngrokUrl.getPublicTunnel();
    }

    this.isAuthenticated = await this.oktaAuth.isAuthenticated();
  }

  async logout(): Promise<void> {
    await this.oktaAuth.signOut();
  }

}
