import { Component, OnInit } from '@angular/core';
import { environment } from 'src/environments/environment';
import { NgrokUrlService } from './services/ngrokUrl/ngrok-url.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'Bamba-admin-pwa';
  url: string = '';

  constructor(private ngrokUrl: NgrokUrlService) {
  }

  ngOnInit(): void {
    if (!environment.production){
      this.url += environment.baseUrl;
    } else {
      this.url += this.ngrokUrl.getPublicTunnel();
    }

    
  }
}