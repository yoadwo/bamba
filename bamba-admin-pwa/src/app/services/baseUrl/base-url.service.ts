import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { NgrokUrlService } from '../ngrokUrl/ngrok-url.service';

@Injectable({
  providedIn: 'root'
})
export class BaseUrlService {
  url: string = '';

  constructor(private ngrokUrl: NgrokUrlService) { 
    if (!environment.production){
      this.url += environment.baseUrl;
    } else {
      if (environment.baseUrl == ''){
        // no baseurl specified - generate from tunnels file
        this.url += this.ngrokUrl.getPublicTunnel();
      } else {
        // baseurl specified, for deployment on cloud
        this.url += environment.baseUrl;
      }
    }
  }
}
