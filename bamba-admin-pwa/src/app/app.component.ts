import { Component, OnInit } from '@angular/core';
import { environment } from 'src/environments/environment';
import { NgrokUrlService } from './services/ngrokUrl/ngrok-url.service';
import { OktaAuthService } from '@okta/okta-angular';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'Bamba-admin-pwa';
  url: string = '';
  isAuthenticated: boolean;


  constructor(
    private ngrokUrl: NgrokUrlService,
    public oktaAuth: OktaAuthService) {
      this.isAuthenticated = false;
      this.oktaAuth.$authenticationState.subscribe(
        (isAuthenticated: boolean) => this.isAuthenticated = isAuthenticated
      )
    }

  ngOnInit(): void {
    if (!environment.production){
      this.url += environment.baseUrl;
    } else {
      this.url += this.ngrokUrl.getPublicTunnel();
    }
    this.oktaAuth.isAuthenticated().then((auth) => {this.isAuthenticated = auth});    
  }

  async login(): Promise<void> {
    await this.oktaAuth.signInWithRedirect();
  }
  
  async logout(): Promise<void> {
    await this.oktaAuth.signOut();
  }
}